namespace Abbas_Behjatnia.Shared.Domain;

public interface IAggregateRoot : IEntity
{

}

public interface IAggregateRoot<TKey> : IEntity<TKey>, IEntity, IAggregateRoot
{

}