using AutoMapper;
using KUHr.Data.Entities.InvoiceTables;
using KUHr.DataAccess;
using KUHr.Service.Dto;
using KUHr.Service.Interface;
using KUHr.Service.Services.Base;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUHr.Service.Services
{
    public class InvoiceItemService : BaseServices, IInvoiceItemService
    {
        public InvoiceItemService(IMapper mapper,
                            IUnitOfWork unitOfWork,
                            IStringLocalizer<Resources.KUHr> kUHrLocalize
                           )
       : base(mapper, unitOfWork, kUHrLocalize)
        {
        }

        public async Task<string> AddInvoiceItem(InvoiceItemDto model)
        {            
            var newinvoices = Mapper.Map<InvoiceItem>(model);
            await UnitOfWork.GetRepository<InvoiceItem>().AddAsync(newinvoices);
            await UnitOfWork.SaveChangesAsync();
            return null;
        }
        public async Task<IEnumerable<InvoiceItemDto>> GetInvoiceItem(Guid invoiceId)
        {
            var invoiceItem = await UnitOfWork.GetRepository<InvoiceItem>().FindAsyncInclude(a => a.InvoiceId == invoiceId);
            return Mapper.Map<IEnumerable<InvoiceItem>, IEnumerable<InvoiceItemDto>>(invoiceItem);
        }
    }
}
