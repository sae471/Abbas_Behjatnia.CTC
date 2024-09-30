

using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.CTC.HttpApi.Controllers;

public class CurrencyUnitController : BaseController<CurrencyUnit, CurrencyUnitOutputDto, CurrencyUnitInputDto>
{
    public new ICurrencyUnitAppService AppService
    {
        get
        {
            base.AppService = LazyServiceProvider.LazyGetService<ICurrencyUnitAppService>();
            return (ICurrencyUnitAppService)base.AppService;
        }
    }
}