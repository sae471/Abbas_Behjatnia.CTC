
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class TaxExemptInputDto : EntityDto<Guid>
{
    public required string Title { get; set; }
    public bool IsExempt { get; set; }
    public bool AmountIsPercentage { get; set; }
    public decimal Amount { get; set; }
    public TimeSpan? FromTime { get; set; }
    public TimeSpan? ToTime { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public DayOfWeek? DayofWeek { get; set; }
    public int? Day { get; set; }
    public int? Week { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    public Guid? ProvinceId { get; set; }
    public Guid? CityId { get; set; }
    public Guid? TollStationId { get; set; }
    public Guid? VehicleCategoryId { get; set; }
    public VehicleType? VehicleType { get; set; }
}