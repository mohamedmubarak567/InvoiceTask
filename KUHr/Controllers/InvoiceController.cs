
using KUHr.Service.Dto;
using KUHr.Service.Interface;
using KUHr.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KUHr.WebAPI.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;

        /// <inheritdoc />
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoice()
        {
            var invoices = await _invoiceService.GetAllInvoice();
            return Ok(invoices);
        }

        [HttpPost]
        public async Task<IActionResult> Add(InvoiceDto invoiceDto)
        {
            var message = await _invoiceService.Add(invoiceDto);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(InvoiceDto invoiceDto)
        {
            var message = await _invoiceService.Update(invoiceDto);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var message = await _invoiceService.Delete(id);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }

    }
}
