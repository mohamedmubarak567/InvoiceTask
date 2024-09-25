using AutoMapper;
using KUHr.Data.Entities.InvoiceTables;
using KUHr.Service.Dto;
using System.Linq;


namespace KUHr.Service.AutoMapper
{
    public partial class KUHrProfile : Profile
    {
        public KUHrProfile()
        {
            CreateMap<InvoiceDto, Invoice>().ReverseMap()
                .ForMember(dest => dest.InvoiceItemCount, opt => opt.MapFrom(src => src.InvoiceItems != null ? src.InvoiceItems.Count() : 0));

            CreateMap<InvoiceItemDto, InvoiceItem>().ReverseMap();
        }
     
    }
}
