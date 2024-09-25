using KUHr.Service.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUHr.Service.Interface
{
    public interface IInvoiceService
    {
        Task<string> Add(InvoiceDto invoiceDto);
        Task<string> Delete(Guid id);
        Task<IEnumerable<InvoiceDto>> GetAllInvoice();
        Task<string> Update(InvoiceDto invoiceDto);
    }
}