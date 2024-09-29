using AutoMapper;
using Abbas_Behjatnia.Shared.Application.Dto;
using Abbas_Behjatnia.Shared.Domain.Entities;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.AspNetCore;

namespace Abbas_Behjatnia.Shared.Application.Services;

public abstract class BaseAppService<TEntity, TEntityDto, TEntityUpsertDto> : IBaseAppService<TEntity, TEntityDto, TEntityUpsertDto>
    where TEntity : Entity<Guid>
    where TEntityDto : EntityDto<Guid>
    where TEntityUpsertDto : EntityDto<Guid>
{
    public IRepository<TEntity> Repository => LazyServiceProvider.LazyGetService<IRepository<TEntity>>();
    public IMapper Mapper => LazyServiceProvider.LazyGetService<IMapper>();

    public virtual async Task<int> DeleteAsync(Guid id)
    {
        return await Repository.DeleteAsync(id);
    }

    public virtual async Task<TEntityDto> GetAsync(Guid id)
    {
        var entity = await Repository.FindAsync(id);
        return Mapper.Map<TEntity, TEntityDto>(entity);
    }

    public virtual Task<ResultDto<TEntityDto>> GetListAsync(RequestDto input)
    {
        return Repository.GetQueryableAsync().Result.ToResultDto<TEntity, TEntityDto>(input);
    }

    public virtual async Task<TEntityDto> UpsertAsync(TEntityUpsertDto input)
    {
        var entity = await Repository.FindAsync(input.Id);
        if (entity == null)
        {
            entity = Mapper.Map<TEntityUpsertDto, TEntity>(input);
            return Mapper.Map<TEntity, TEntityDto>(await Repository.InsertAsync(entity));
        }

        entity = Mapper.Map<TEntityUpsertDto, TEntity>(input);
        return Mapper.Map<TEntity, TEntityDto>(await Repository.UpdateAsync(entity));
    }
}
