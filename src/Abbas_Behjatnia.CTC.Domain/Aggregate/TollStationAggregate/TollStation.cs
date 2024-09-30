using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class TollStation : AggregateRoot<Guid>
{
    public virtual string Title { get; internal set; }
    public virtual Guid? ProvinceId { get; internal set; }
    public virtual CountryDivision Province { get; internal set; }
    public virtual Guid? CityId { get; internal set; }
    public virtual CountryDivision City { get; internal set; }

    protected TollStation() { }
    public TollStation(Guid id, string title, Guid provinceId, Guid cityId) : base(id)
    {
        this.Id = id;
        this.Title = title;
        this.ProvinceId = provinceId;
        this.CityId = cityId;
    }
}