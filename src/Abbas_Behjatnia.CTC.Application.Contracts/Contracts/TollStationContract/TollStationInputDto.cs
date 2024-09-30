
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class TollStationInputDto : EntityDto<Guid>
{
    public required string Title { get; set; }
    public required Guid ProvinceId { get; set; }
    public required Guid CityId { get; set; }
}