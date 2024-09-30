

using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.CTC.HttpApi.Controllers;

public class TollStationController : BaseController<TollStation, TollStationOutputDto, TollStationInputDto>
{
    public new ITollStationAppService AppService
    {
        get
        {
            base.AppService = LazyServiceProvider.LazyGetService<ITollStationAppService>();
            return (ITollStationAppService)base.AppService;
        }
    }
}