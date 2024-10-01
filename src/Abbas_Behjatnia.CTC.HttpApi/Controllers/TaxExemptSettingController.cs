

using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.CTC.HttpApi.Controllers;

public class TaxExemptSettingController : BaseController<TaxExemptSetting, TaxExemptSettingOutputDto, TaxExemptSettingInputDto>
{
    public new ITaxExemptSettingAppService AppService
    {
        get
        {
            base.AppService = LazyServiceProvider.LazyGetService<ITaxExemptSettingAppService>();
            return (ITaxExemptSettingAppService)base.AppService;
        }
    }
}