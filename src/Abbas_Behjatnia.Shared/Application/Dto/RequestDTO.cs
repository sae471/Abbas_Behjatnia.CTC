
namespace Abbas_Behjatnia.Shared.Application.Dto;

public class RequestDto
{
    public string? Filtering { get; set; }
    public string? Fields { get; set; }
    public string? LookupStringFormat { get; set; }
    public string? SearchedFields { get; set; }
    public string? SearchedText { get; set; }
    public string? Sorting { get; set; }
    public int? SkipCount { get; set; } = 0;
    public int? MaxResultCount { get; set; } = int.MaxValue;
}