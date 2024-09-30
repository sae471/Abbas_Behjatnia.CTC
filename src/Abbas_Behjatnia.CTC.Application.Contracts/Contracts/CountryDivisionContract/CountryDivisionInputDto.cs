
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class CountryDivisionInputDto : EntityDto<Guid>
{
    public CountryDivisionType Type { get; set; }
    public required string Name { get; set; }
    public Guid? ParentId { get; set; } = default;
}