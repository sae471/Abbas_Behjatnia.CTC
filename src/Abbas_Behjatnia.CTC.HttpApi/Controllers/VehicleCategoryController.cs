

using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.CTC.HttpApi.Controllers;

public class VehicleCategoryController : BaseController<VehicleCategory, VehicleCategoryOutputDto, VehicleCategoryInputDto>
{
    public new IVehicleCategoryAppService AppService
    {
        get
        {
            base.AppService = LazyServiceProvider.LazyGetService<IVehicleCategoryAppService>();
            return (IVehicleCategoryAppService)base.AppService;
        }
    }
}