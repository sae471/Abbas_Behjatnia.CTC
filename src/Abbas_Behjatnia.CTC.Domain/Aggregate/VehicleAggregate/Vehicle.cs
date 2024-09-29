using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class Vehicle : AggregateRoot<Guid>
{
    public virtual VehicleType Type { get; internal set; }
    public virtual VehicleOwnerShipType OwnerShipType { get; internal set; }
    public virtual string PlateNumber { get; internal set; }
    public virtual string ChassisNumber { get; internal set; }
    public virtual string ManufacturerCompany { get; set; }
    public virtual string ManufacturerClass { get; set; }
    public virtual string YearofManufacture { get; set; }
    public virtual Guid? VehicleCategoryId { get; internal set; }
    public virtual VehicleCategory VehicleCategory { get; internal set; }
    public virtual string Color { get; set; }

    protected Vehicle() { }
    public Vehicle(Guid id, VehicleType type, VehicleOwnerShipType ownerShipType, string plateNumber, string chassisNumber) : base(id)
    {
        Id = id;
        Type = type;
        OwnerShipType = ownerShipType;
        PlateNumber = plateNumber;
        ChassisNumber = chassisNumber;
    }
}