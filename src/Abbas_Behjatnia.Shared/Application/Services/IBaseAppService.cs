
using Abbas_Behjatnia.Shared.Application.Dto;
using Abbas_Behjatnia.Shared.Domain.Entities;


namespace Abbas_Behjatnia.Shared.Application.Services;

public interface IBaseAppService<TEntity, TEntityDto, TEntityUpsertDto>
    where TEntity : Entity<Guid>
    where TEntityDto : EntityDto<Guid>
    where TEntityUpsertDto : EntityDto<Guid>
{
    Task<IQueryable<TEntity>> GetQueryableAsync();
    Task<TEntityDto> UpsertAsync(TEntityUpsertDto input);
    Task<TEntityDto> GetAsync(Guid id);
    Task<ResultDto<TEntityDto>> GetListAsync(RequestDto input);
    Task<int> DeleteAsync(Guid id);
}