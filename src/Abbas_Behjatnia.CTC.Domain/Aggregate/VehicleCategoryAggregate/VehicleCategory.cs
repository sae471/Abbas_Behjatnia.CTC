using System;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class VehicleCategory : AggregateRoot<Guid>
{
    public virtual string Name { get; internal set; }
    public virtual Guid? ParentId { get; internal set; }
    public virtual VehicleCategory Parent { get; internal set; }

    protected VehicleCategory() { }
    public VehicleCategory(Guid id, string name) : base(id)
    {
        Id = id;
        Name = name;
    }
}