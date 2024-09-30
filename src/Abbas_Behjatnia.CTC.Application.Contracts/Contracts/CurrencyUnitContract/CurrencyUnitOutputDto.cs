
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class CurrencyUnitOutputDto : EntityDto<Guid>
{
    public required string Name { get; set; }
    public int DecimalNumber { get; set; }
    public string? Symbol { get; set; }
}