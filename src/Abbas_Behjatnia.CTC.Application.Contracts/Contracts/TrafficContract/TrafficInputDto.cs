
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class TrafficInputDto : EntityDto<Guid>
{
    public DateTime DateTime { get; set; }
    public Guid VehicleId { get; set; }
    public Guid TollStationId { get; set; }
}