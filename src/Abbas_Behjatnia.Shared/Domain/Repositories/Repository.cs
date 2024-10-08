using Microsoft.EntityFrameworkCore;
using Abbas_Behjatnia.Shared.Domain.Entities;
using Abbas_Behjatnia.Shared.AspNetCore;
using System.Linq.Expressions;

namespace Abbas_Behjatnia.Shared.Domain.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity<Guid>
{
    private DbContext _context => LazyServiceProvider.LazyGetService<DbContext>();
    public virtual Task<int> DeleteAsync(Guid id)
    {
        var entity = _context.Set<TEntity>().SingleOrDefault(it => it.Id == id);
        if (entity == null)
        {
            throw new Exception("the entity not be found!!");
        }
        _context.Remove(entity);

        return Task.Run(() =>
        {
            return _context.SaveChangesAsync();
        });

        // return Task.Run(()=>{ await _context.SaveChangesAsync()})
    }
    public virtual async Task<TEntity> FindAsync(Guid id)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(it => it.Id == id);
    }
    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }
    public virtual Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        return Task.Run(() =>
        {
            return _context.Set<TEntity>().AsQueryable();
        });
    }

    public virtual DbSet<TEntity> Set()
    {
        return _context.Set<TEntity>();
    }
    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        var result = await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var _entity = _context.Set<TEntity>().First(it => it.Id == entity.Id);
        _entity = entity;
        await _context.SaveChangesAsync();
        return _entity;
    }
}
