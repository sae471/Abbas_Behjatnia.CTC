
namespace Abbas_Behjatnia.Shared.Domain.Entities;

public interface IEntity<TKey>
{
    TKey Id { get; }
}
