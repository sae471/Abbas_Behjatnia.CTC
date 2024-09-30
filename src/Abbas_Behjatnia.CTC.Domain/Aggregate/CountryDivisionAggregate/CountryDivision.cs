using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class CountryDivision : AggregateRoot<Guid>
{
    public virtual CountryDivisionType Type { get; internal set; }
    public virtual string Name { get; internal set; }
    public virtual Guid? ParentId { get; internal set; }
    public virtual CountryDivision Parent { get; internal set; }

    protected CountryDivision() { }
    public CountryDivision(Guid id, CountryDivisionType type, string name) : base(id)
    {
        this.Id = id;
        this.Name = name;
        this.Type = type;
    }
}