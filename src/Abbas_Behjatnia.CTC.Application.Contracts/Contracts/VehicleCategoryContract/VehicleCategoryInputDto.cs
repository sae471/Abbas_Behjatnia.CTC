
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class VehicleCategoryInputDto : EntityDto<Guid>
{
    public required string Name { get; set; }
    public Guid? ParentId { get; set; }
}