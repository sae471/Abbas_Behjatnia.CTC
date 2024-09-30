using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Domain.Entities;
using Abbas_Behjatnia.Shared.Domain.Entities.Auditing;


namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class Traffic : AggregateRoot<Guid>
{
    public virtual DateTime DateTime { get; internal set; }
    public virtual Guid VehicleId { get; internal set; }
    public virtual Vehicle Vehicle { get; internal set; }
    public virtual Guid TollStationId { get; internal set; }
    public virtual TollStation TollStation { get; internal set; }


    protected Traffic() { }
    public Traffic(Guid id, DateTime dateTime, Guid vehicleId, Guid tollStationId) : base(id)
    {
        this.Id = id;
        this.VehicleId = vehicleId;
        this.TollStationId = tollStationId;
        this.DateTime = dateTime;
    }
}