
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Services;
using Microsoft.AspNetCore.Razor.Language;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class TaxExemptSettingAppService : BaseAppService<TaxExemptSetting, TaxExemptSettingOutputDto, TaxExemptSettingInputDto>, ITaxExemptSettingAppService
{
    private TaxExemptSettingManager _taxExemptSettingManager => (TaxExemptSettingManager)LazyServiceProvider.LazyGetService<IDomainService<TaxExemptSetting>>();

    public override async Task<TaxExemptSettingOutputDto> UpsertAsync(TaxExemptSettingInputDto input)
    {
        var isNew = false;
        var taxExemptSetting = await Repository.FindAsync(input.Id);
        if (taxExemptSetting == null)
        {
            isNew = true;
            taxExemptSetting = _taxExemptSettingManager.New(input.Type, input.Value);
        }
        if (!isNew)
        {
            _taxExemptSettingManager.SetTaxExemptSettingType(taxExemptSetting, input.Type);
            _taxExemptSettingManager.SetValue(taxExemptSetting, input.Value);
            return Mapper.Map<TaxExemptSetting, TaxExemptSettingOutputDto>(await Repository.UpdateAsync(taxExemptSetting));
        }
        return Mapper.Map<TaxExemptSetting, TaxExemptSettingOutputDto>(await Repository.InsertAsync(taxExemptSetting));
    }

    public async Task NormalizationByMaximumTaxAmountPerDay(List<VehicleTrafficTaxMOutputDto> trafficTaxlist)
    {
        var relatedSetting = await Repository.FindAsync(it => it.Type == TaxExemptSettingType.MaximumTaxAmountPerDay);
        if (relatedSetting == null)
        {
            return;
        }
        var maxTaxPerDay = decimal.Parse(relatedSetting.Value);
        var DatesOfTrafficList = trafficTaxlist
            .Select(it => it.DateTime.Date).Distinct().ToList();

        foreach (var date in DatesOfTrafficList)
        {
            var sumOfAppliedTax = trafficTaxlist.Where(it => it.DateTime.Date == date).Sum(it => it.AppliedTax);
            var surplusTaxPerDay = sumOfAppliedTax - maxTaxPerDay;
            if (surplusTaxPerDay <= 0)
            {
                continue;
            }
            foreach (var item in trafficTaxlist.Where(it => it.DateTime.Date == date).OrderByDescending(it => it.DateTime).ToList())
            {
                if (surplusTaxPerDay <= 0)
                {
                    continue;
                }
                item.SurplusTax += item.AppliedTax > surplusTaxPerDay ? surplusTaxPerDay : item.AppliedTax;
                item.AppliedTax = item.AppliedTax > surplusTaxPerDay ? item.AppliedTax - surplusTaxPerDay : 0;
                trafficTaxlist
                .Where(it => it.Id == item.Id)
                .Select(it =>
                {
                    it.SurplusTax = item.SurplusTax;
                    it.AppliedTax = item.AppliedTax;
                    return it;
                })
                .ToList();
                surplusTaxPerDay -= item.SurplusTax;
            }
        }


    }

    public async Task NormalizationNumberOfAppliedTrafficInMaximumValuePerHoure(List<VehicleTrafficTaxMOutputDto> trafficTaxlist)
    {
        var relatedSetting = await Repository.FindAsync(it => it.Type == TaxExemptSettingType.NumberOfAppliedTrafficInMaximumValuePerHoure);
        if (relatedSetting == null)
        {
            return;
        }
        var skip = int.Parse(relatedSetting.Value);
        var trafficTimeList = trafficTaxlist
            .Select(it => new DateTime(it.DateTime.Year, it.DateTime.Month, it.DateTime.Day, it.DateTime.Hour, 0, 0)).Distinct().ToList();

        trafficTimeList.ForEach(trafficTime =>
        {
            trafficTaxlist
                .Where(it => it.DateTime.Date == trafficTime.Date && it.DateTime.Hour == trafficTime.Hour)
                .OrderByDescending(it => it.AppliedTax)
                .Skip(skip)
                .Select(it =>
                {
                    it.SurplusTax += it.AppliedTax;
                    it.AppliedTax = 0;
                    return it;
                })
                .ToList();
        });
    }


}