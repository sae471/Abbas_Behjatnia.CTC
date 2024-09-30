using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;
using Abbas_Behjatnia.Shared.Domain.Entities.Auditing;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class TaxExempt : AggregateRoot<Guid>
{
    public string Title { get; set; }
    public bool IsExempt { get; set; }
    public bool ValueIsPercentage { get; set; }
    public decimal Value { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public DayofWeek DayofWeek { get; set; }
    public int Day { get; set; }
    public int Week { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Guid? ProvinceId { get; set; }
    public CountryDivision Province { get; set; }
    public Guid? CityId { get; set; }
    public CountryDivision City { get; set; }
    public Guid? TollStationId { get; set; }
    public TollStation TollStation { get; set; }
    public Guid? VehicleCategoryId { get; set; }
    public VehicleCategory VehicleCategory { get; set; }
    public VehicleType VehicleType { get; set; }


    protected TaxExempt() { }
    // public TaxExempt(Guid id, Guid vehicleId, Guid tollStationId, DateTime dateTime) : base(id)
    // {
    //     this.Id = id;
    //     this.VehicleId = vehicleId;
    //     this.TollStationId = tollStationId;
    //     this.DateTime = dateTime;
    // }
}