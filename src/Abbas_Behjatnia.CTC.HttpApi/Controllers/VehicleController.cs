

using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.CTC.HttpApi.Controllers;

public class VehicleController : BaseController<Vehicle, VehicleOutputDto, VehicleInputDto>
{
    public new IVehicleAppService AppService
    {
        get
        {
            base.AppService = LazyServiceProvider.LazyGetService<IVehicleAppService>();
            return (IVehicleAppService)base.AppService;
        }
    }
}