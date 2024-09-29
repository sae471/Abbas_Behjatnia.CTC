
using Microsoft.AspNetCore.Mvc;
using Abbas_Behjatnia.Shared.Application.Dto;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.Domain.Entities;

namespace Abbas_Behjatnia.Shared.AspNetCore;

[ApiController]
[Route("api/[controller]")]
public class BaseController<TEntity, TEntityDto, TEntityUpsertDto> : ControllerBase
where TEntity : Entity<Guid>
    where TEntityDto : EntityDto<Guid>
    where TEntityUpsertDto : EntityDto<Guid>
{
    private IBaseAppService<TEntity, TEntityDto, TEntityUpsertDto> _appService;
    public IBaseAppService<TEntity, TEntityDto, TEntityUpsertDto> AppService
    {
        get => _appService ?? (_appService =
                   LazyServiceProvider.LazyGetService<IBaseAppService<TEntity, TEntityDto, TEntityUpsertDto>>());
        set => _appService = value;
    }

    [HttpGet("getbyid")]
    public virtual async Task<IActionResult> Get(Guid id)
    {
        if (id == null || id == default(Guid))
        {
            return BadRequest();
        }
        var result = await AppService.GetAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("getlist")]
    public virtual async Task<IActionResult> GetListAsync([FromQuery] RequestDto input)
    {
        if (input.MaxResultCount <= 1 || input.SkipCount < 0)
        {
            return BadRequest();
        }
        return Ok(await AppService.GetListAsync(input));
    }

    [HttpPost("upsert")]
    public virtual async Task<IActionResult> Upsert(TEntityUpsertDto input)
    {
        return Ok(await AppService.UpsertAsync(input));
    }

    [HttpDelete("delete")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        return Ok(await AppService.DeleteAsync(id));
    }
}