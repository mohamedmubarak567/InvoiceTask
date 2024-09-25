using AutoMapper;
using KUHr.Data.Entities.InvoiceTables;
using KUHr.DataAccess;
using KUHr.Service.Dto;
using KUHr.Service.Interface;
using KUHr.Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KUHr.Service.Services
{
    public class InvoiceService : BaseServices, IInvoiceService
    {
        public InvoiceService(IMapper mapper,
                            IUnitOfWork unitOfWork,
                            IStringLocalizer<Resources.KUHr> kUHrLocalize
                           )
       : base(mapper, unitOfWork, kUHrLocalize)
        {
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoice()
        {
            var list = await UnitOfWork.GetRepository<Invoice>().FindAsyncInclude(null, include: src => src.Include(r => r.InvoiceItems));
            var listDto = Mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceDto>>(list);
            return listDto;
        }

        public async Task<string> Add(InvoiceDto invoiceDto)
        {
            var invoice = Mapper.Map<InvoiceDto, Invoice>(invoiceDto);
            await UnitOfWork.GetRepository<Invoice>().AddAsync(invoice);
            await UnitOfWork.SaveChangesAsync();
            return null;
        }

        public async Task<string> Update(InvoiceDto invoiceDto)
        {
            var invoiceOld = await UnitOfWork.GetRepository<Invoice>().FirstOrDefaultAsync(a => a.Id == invoiceDto.Id);
            var invoiceNew = Mapper.Map<Invoice>(invoiceDto);
            await UnitOfWork.GetRepository<Invoice>().UpdateAsync(invoiceNew, invoiceOld);
            await UnitOfWork.SaveChangesAsync();
            return null;
        }

        public async Task<string> Delete(Guid id)
        {
            var invoice = await UnitOfWork.GetRepository<Invoice>().FirstOrDefaultAsync(a => a.Id == id, include: source => source.Include(a => a.InvoiceItems));
            if (invoice != null && invoice.InvoiceItems.Count > 0)
            {
                return KUHrLocalize["CanNotDeleteInvoice"];
            }

            await UnitOfWork.GetRepository<Invoice>().RemoveAsync(invoice);
            await UnitOfWork.SaveChangesAsync();
            return null;
        }


    }
}
