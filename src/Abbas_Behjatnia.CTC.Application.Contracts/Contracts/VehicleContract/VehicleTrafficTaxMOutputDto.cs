using System;

using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;

public class VehicleTrafficTaxListMOutputDto 
{
    public decimal TotalTax { get; set; }
    public decimal Exempt { get; set; }
    public decimal SurplusTax { get; set; }
    public decimal AppliedTax { get; set; }
    public List<VehicleTrafficTaxMOutputDto> VehicleTrafficTaxMOutputDto { get; set; } = new List<VehicleTrafficTaxMOutputDto>();

}
public class VehicleTrafficTaxMOutputDto : EntityDto<Guid>
{
    public DateTime DateTime { get; set; }
    public Guid TollStationId { get; set; }
    public required string TollStationTitle { get; set; }
    public Guid ProvinceId { get; set; }
    public Guid CityId { get; set; }
    public decimal TotalTax { get; set; }
    public decimal Exempt { get; set; }
    public decimal SurplusTax { get; set; }
    public decimal AppliedTax { get; set; }
    public List<TaxExemptOutputDto>? TaxExemptList { get; set; }

}