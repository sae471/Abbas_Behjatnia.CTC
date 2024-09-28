
namespace Abbas_Behjatnia.Shared.Domain.Entities;

public class Entity<TKey> : IEntity<TKey>
{
    protected Entity() { }
    protected Entity(TKey id) { }

    public virtual TKey Id { get; protected set; }
}
