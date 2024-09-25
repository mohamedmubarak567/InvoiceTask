using Invoice.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;


public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;

    }
    public List<InvoiceDto> Invoices { get; set; } = new List<InvoiceDto>();
    public List<InvoiceItemDto> invoiceItem { get; set; } = new List<InvoiceItemDto>();

    public async Task OnGet()
    {
        using (var httpClient = new HttpClient())
        {
            using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:5001/api/Invoice/GetAllInvoice"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Invoices = JsonConvert.DeserializeObject<List<InvoiceDto>>(apiResponse);
            }

        }

    }
    public async Task<IActionResult> OnPostCreate(InvoiceDto invoiceDto)
    {
        using (var httpClient = new HttpClient())
        {
            var jsonContent = JsonConvert.SerializeObject(invoiceDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:5001/api/Invoice/Add", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Error posting data: {response.StatusCode}");
            }
        }
        return RedirectToPage();
    }


    public async Task<IActionResult> OnPostUpdate(InvoiceDto invoiceDto)
    {
        using (var httpClient = new HttpClient())
        {
            var jsonContent = JsonConvert.SerializeObject(invoiceDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync("https://localhost:5001/api/Invoice/Update", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Error posting data: {response.StatusCode}");
            }
        }
        return RedirectToPage();
    }
    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.DeleteAsync($"https://localhost:5001/api/Invoice/Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to delete the invoice.");
                return Page();
            }
        }
    }
    public async Task<IActionResult> OnGetInvoiceItems(Guid invoiceId)
    {
        using (var httpClient = new HttpClient())
        {
            using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:5001/api/InvoiceItem/GetInvoiceItem/{invoiceId}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    // Log or handle the status code as needed
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error fetching invoice items: {response.StatusCode} - {errorResponse}");
                    return BadRequest(new { message = "Failed to retrieve invoice items." });
                }

                string apiResponse = await response.Content.ReadAsStringAsync();
                invoiceItem = JsonConvert.DeserializeObject<List<InvoiceItemDto>>(apiResponse);
            }
        }
        return new JsonResult(invoiceItem);
    }

    public async Task<IActionResult> OnPostAddInvoiceItem([FromBody] InvoiceItemDto invoiceItemDto)
    {
        using (var httpClient = new HttpClient())
        {
            var jsonContent = JsonConvert.SerializeObject(invoiceItemDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:5001/api/InvoiceItem/AddInvoiceItem", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Error posting data: {response.StatusCode}");
            }
        }
        return RedirectToPage();
    }

}




