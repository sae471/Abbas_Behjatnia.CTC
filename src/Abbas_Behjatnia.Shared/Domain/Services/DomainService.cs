
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Entities;
using Abbas_Behjatnia.Shared.Domain.Repositories;

namespace Abbas_Behjatnia.Shared.Domain.Services;

public abstract class DomainService<TEntity> : IDomainService<TEntity> where TEntity : Entity<Guid>
{
    public IRepository<TEntity> Repository => LazyServiceProvider.LazyGetService<IRepository<TEntity>>();
}