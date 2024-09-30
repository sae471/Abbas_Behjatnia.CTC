
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class CurrencyUnitAppService : BaseAppService<CurrencyUnit, CurrencyUnitOutputDto, CurrencyUnitInputDto>, ICurrencyUnitAppService
{
    private CurrencyUnitManager _currencyUnitManager => (CurrencyUnitManager)LazyServiceProvider.LazyGetService<IDomainService<CurrencyUnit>>();
    public override async Task<CurrencyUnitOutputDto> UpsertAsync(CurrencyUnitInputDto input)
    {
        var isNew = false;
        var currencyUnit = await Repository.FindAsync(input.Id);
        if (currencyUnit == null)
        {
            isNew = true;
            currencyUnit = _currencyUnitManager.New(input.Name, input.DecimalNumber);
        }

        currencyUnit.Symbol = input.Symbol;

        if (!isNew)
        {
            _currencyUnitManager.SetName(currencyUnit, input.Name);
            _currencyUnitManager.SetDecimalNumber(currencyUnit, input.DecimalNumber);
            return Mapper.Map<CurrencyUnit, CurrencyUnitOutputDto>(await Repository.UpdateAsync(currencyUnit));
        }
        return Mapper.Map<CurrencyUnit, CurrencyUnitOutputDto>(await Repository.InsertAsync(currencyUnit));
    }
}