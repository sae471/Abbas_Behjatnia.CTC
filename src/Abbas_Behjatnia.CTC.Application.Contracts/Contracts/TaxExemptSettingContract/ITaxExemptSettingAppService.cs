using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public interface ITaxExemptSettingAppService : IBaseAppService<TaxExemptSetting, TaxExemptSettingOutputDto, TaxExemptSettingInputDto>
{
    Task NormalizationByMaximumTaxAmountPerDay(List<VehicleTrafficTaxMOutputDto> trafficTaxlist);
    Task NormalizationNumberOfAppliedTrafficInMaximumValuePerHoure(List<VehicleTrafficTaxMOutputDto> trafficTaxlist);

}