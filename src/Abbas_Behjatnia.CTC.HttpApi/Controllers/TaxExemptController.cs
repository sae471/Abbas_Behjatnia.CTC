

using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.CTC.HttpApi.Controllers;

public class TaxExemptController : BaseController<TaxExempt, TaxExemptOutputDto, TaxExemptInputDto>
{
    public new ITaxExemptAppService AppService
    {
        get
        {
            base.AppService = LazyServiceProvider.LazyGetService<ITaxExemptAppService>();
            return (ITaxExemptAppService)base.AppService;
        }
    }
}