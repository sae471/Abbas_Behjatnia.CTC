

using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.CTC.HttpApi.Controllers;

public class TrafficController : BaseController<Traffic, TrafficOutputDto, TrafficInputDto>
{
    public new ITrafficAppService AppService
    {
        get
        {
            base.AppService = LazyServiceProvider.LazyGetService<ITrafficAppService>();
            return (ITrafficAppService)base.AppService;
        }
    }
}