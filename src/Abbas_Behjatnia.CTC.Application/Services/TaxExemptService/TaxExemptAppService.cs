
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class TaxExemptAppService : BaseAppService<TaxExempt, TaxExemptOutputDto, TaxExemptInputDto>, ITaxExemptAppService
{
    private TaxExemptManager _taxExemptManager => (TaxExemptManager)LazyServiceProvider.LazyGetService<IDomainService<TaxExempt>>();
    public override async Task<TaxExemptOutputDto> UpsertAsync(TaxExemptInputDto input)
    {
        var isNew = false;
        var taxExempt = await Repository.FindAsync(input.Id);
        if (taxExempt == null)
        {
            isNew = true;
            taxExempt = await _taxExemptManager.NewAsync(input.Title, input.Amount, input.IsExempt, input.AmountIsPercentage,input.CurrencyUnitId);
        }

        taxExempt.From = input.From??default;
        taxExempt.To = input.To??default;
        _taxExemptManager.SetDayofWeek(taxExempt, input.DayofWeek??default);
        taxExempt.Day = input.Day??default;
        taxExempt.Week = input.Week??default;
        taxExempt.Month = input.Month??default;
        taxExempt.Year = input.Year??default;

        await _taxExemptManager.SetProvinceAsync(taxExempt, input.ProvinceId??default);
        await _taxExemptManager.SetCityAsync(taxExempt, input.CityId??default);
        await _taxExemptManager.SetTollStationAsync(taxExempt, input.TollStationId??default);
        await _taxExemptManager.SetVehicleCategoryAsync(taxExempt, input.VehicleCategoryId??default);
        _taxExemptManager.SetVehicleType(taxExempt, input.VehicleType??default);

        if (!isNew)
        {
            _taxExemptManager.SetTitle(taxExempt, input.Title);
            _taxExemptManager.SetAmount(taxExempt, input.Amount);
            taxExempt.IsExempt = input.IsExempt;
            taxExempt.AmountIsPercentage = input.AmountIsPercentage;
            return Mapper.Map<TaxExempt, TaxExemptOutputDto>(await Repository.UpdateAsync(taxExempt));
        }
        return Mapper.Map<TaxExempt, TaxExemptOutputDto>(await Repository.InsertAsync(taxExempt));
    }
}