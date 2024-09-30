using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class CurrencyUnit : AggregateRoot<Guid>
{
    public virtual string Name { get; internal set; }
    public virtual int DecimalNumber { get; internal set; }
    public virtual string Symbol { get; set; }

    protected CurrencyUnit() { }
    public CurrencyUnit(Guid id, string name,int decimalٔNumber) : base(id)
    {
        this.Id = id;
        this.Name = name;
        this.DecimalNumber = decimalٔNumber;
    }
}