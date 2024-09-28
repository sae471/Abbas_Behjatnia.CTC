using System.Linq.Expressions;
using Abbas_Behjatnia.Shared.Domain.Entities;

namespace Abbas_Behjatnia.Shared.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : IEntity<Guid>
{
    Task<TEntity> FindAsync(Guid id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<int> DeleteAsync(Guid id);
    Task<IQueryable<TEntity>> GetQueryableAsync();
    Task<TEntity> FirstOrDefaultAsync(Guid id);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
}
