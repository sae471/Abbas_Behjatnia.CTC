

namespace Abbas_Behjatnia.Shared.Application.Dto;
public abstract class EntityDto<TKey> : IEntityDto<TKey>
{
    protected EntityDto() { }

    public TKey Id { get; set; }
}