using System;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;
using Abbas_Behjatnia.Shared.Domain.Entities.Auditing;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class TaxExempt : AggregateRoot<Guid>
{
    public virtual string Title { get; internal set; }
    public virtual bool IsExempt { get; set; }
    public virtual bool AmountIsPercentage { get; set; }
    public virtual decimal Amount { get; internal set; }
    public virtual Guid CurrencyUnitId { get; internal set; }
    public virtual CurrencyUnit CurrencyUnit { get; internal set; }
    public virtual DateTime From { get; set; }
    public virtual DateTime To { get; set; }
    public virtual DayofWeek DayofWeek { get; internal set; }
    public virtual int Day { get; set; }
    public virtual int Week { get; set; }
    public virtual int Month { get; set; }
    public virtual int Year { get; set; }
    public virtual Guid? ProvinceId { get; internal set; }
    public virtual CountryDivision Province { get; internal set; }
    public virtual Guid? CityId { get; internal set; }
    public virtual CountryDivision City { get; internal set; }
    public virtual Guid? TollStationId { get; internal set; }
    public virtual TollStation TollStation { get; internal set; }
    public virtual Guid? VehicleCategoryId { get; internal set; }
    public virtual VehicleCategory VehicleCategory { get; internal set; }
    public virtual VehicleType VehicleType { get; internal set; }


    protected TaxExempt() { }
    public TaxExempt(Guid id, string title, decimal amount, Guid currencyUnitId) : base(id)
    {
        this.Id = id;
        this.Title = title;
        this.Amount = amount;
        this.CurrencyUnitId = currencyUnitId;
    }
}