using System;

using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class TaxExemptOutputDto : EntityDto<Guid>
{
    public required string Title { get; set; }
    public bool IsExempt { get; set; }
    public bool AmountIsPercentage { get; set; }
    public decimal Amount { get; set; }
    public required KeyValueDto<Guid> CurrencyUnit { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public DayofWeek DayofWeek { get; set; }
    public int Day { get; set; }
    public int Week { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public KeyValueDto<Guid>? Province { get; set; }
    public KeyValueDto<Guid>? City { get; set; }
    public KeyValueDto<Guid>? TollStation { get; set; }
    public KeyValueDto<Guid>? VehicleCategory { get; set; }
    public VehicleType VehicleType { get; set; }

}