using KUHr.Service.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUHr.Service.Interface
{
    public interface IInvoiceItemService
    {
        Task<string> AddInvoiceItem(InvoiceItemDto model);
        Task<IEnumerable<InvoiceItemDto>> GetInvoiceItem(Guid invoiceId);
    }
}