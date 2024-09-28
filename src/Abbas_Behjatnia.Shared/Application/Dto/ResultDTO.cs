
namespace Abbas_Behjatnia.Shared.Application.Dto;

public class ResultDto<T>
{
    public long TotalCount { get; set; }
    public IReadOnlyList<T> Items { get; set; }
}