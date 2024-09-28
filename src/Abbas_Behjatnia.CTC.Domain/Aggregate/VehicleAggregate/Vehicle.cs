using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class Vehicle : AggregateRoot<Guid>
{
    public virtual VehicleType Type { get; internal set; } = 0;
    public virtual string PlateNumber { get; internal set; } = string.Empty;

    protected Vehicle() { }
    public Vehicle(Guid id, string plateNumber, VehicleType type) : base(id)
    {
        PlateNumber = plateNumber;
        Type = type;
    }
}