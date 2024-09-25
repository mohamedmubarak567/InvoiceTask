using AutoMapper;
using KUHr.DataAccess;
using Microsoft.Extensions.Localization;

namespace KUHr.Service.Services.Base
{
    public class BaseServices
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IStringLocalizer<Service.Resources.KUHr> KUHrLocalize;

        public BaseServices(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<Service.Resources.KUHr> kuhrlocalize)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            KUHrLocalize = kuhrlocalize;
        }
    }
}
