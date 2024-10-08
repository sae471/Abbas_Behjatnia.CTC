using System.Linq.Expressions;
using Abbas_Behjatnia.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Abbas_Behjatnia.Shared.Domain.Repositories;

public interface IRepository<TEntity>
    where TEntity : class, IEntity<Guid>
{
    Task<TEntity> FindAsync(Guid id);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<int> DeleteAsync(Guid id);
    Task<IQueryable<TEntity>> GetQueryableAsync();
    DbSet<TEntity> Set();
}
