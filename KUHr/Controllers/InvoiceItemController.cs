
using KUHr.Service.Dto;
using KUHr.Service.Interface;
using KUHr.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUHr.WebAPI.Controllers
{
    public class InvoiceItemController : BaseController
    {
        private readonly IInvoiceItemService _invoiceItemService;

        /// <inheritdoc />
        public InvoiceItemController(IInvoiceItemService invoiceItemService)
        {
            _invoiceItemService = invoiceItemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoiceItem(InvoiceItemDto model)
        {
            var message = await _invoiceItemService.AddInvoiceItem(model);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }
        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoiceItem(Guid invoiceId)
        {
            var invoiceItem = await _invoiceItemService.GetInvoiceItem(invoiceId);
            return Ok(invoiceItem);
        }
    }
}
