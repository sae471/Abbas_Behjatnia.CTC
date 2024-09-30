
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class CountryDivisionAppService : BaseAppService<CountryDivision, CountryDivisionOutputDto, CountryDivisionInputDto>, ICountryDivisionAppService
{
    private CountryDivisionManager _countryDivisionManager => (CountryDivisionManager)LazyServiceProvider.LazyGetService<IDomainService<CountryDivision>>();
    public override async Task<CountryDivisionOutputDto> UpsertAsync(CountryDivisionInputDto input)
    {
        var isNew = false;
        var countryDivision = await Repository.FindAsync(input.Id);
        if (countryDivision == null)
        {
            isNew = true;
            countryDivision = _countryDivisionManager.New(input.Type, input.Name);
        }
        await _countryDivisionManager.SetParentAsync(countryDivision, input.ParentId ?? default);

        if (!isNew)
        {
            _countryDivisionManager.SetName(countryDivision, input.Name);
            _countryDivisionManager.setCountryDivisionType(countryDivision, input.Type);
            return Mapper.Map<CountryDivision, CountryDivisionOutputDto>(await Repository.UpdateAsync(countryDivision));
        }
        return Mapper.Map<CountryDivision, CountryDivisionOutputDto>(await Repository.InsertAsync(countryDivision));
    }
}