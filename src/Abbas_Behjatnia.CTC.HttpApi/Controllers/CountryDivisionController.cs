

using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.CTC.HttpApi.Controllers;

public class CountryDivisionController : BaseController<CountryDivision, CountryDivisionOutputDto, CountryDivisionInputDto>
{
    public new ICountryDivisionAppService AppService
    {
        get
        {
            base.AppService = LazyServiceProvider.LazyGetService<ICountryDivisionAppService>();
            return (ICountryDivisionAppService)base.AppService;
        }
    }
}