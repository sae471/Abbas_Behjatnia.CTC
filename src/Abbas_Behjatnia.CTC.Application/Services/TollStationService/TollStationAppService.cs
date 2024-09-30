
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class TollStationAppService : BaseAppService<TollStation, TollStationOutputDto, TollStationInputDto>, ITollStationAppService
{
    private TollStationManager _tollStationManager => (TollStationManager)LazyServiceProvider.LazyGetService<IDomainService<TollStation>>();
    public override async Task<TollStationOutputDto> UpsertAsync(TollStationInputDto input)
    {
        var isNew = false;
        var tollStation = await Repository.FindAsync(input.Id);
        if (tollStation == null)
        {
            isNew = true;
            tollStation = await _tollStationManager.NewAsync(input.Title, input.ProvinceId, input.CityId);
        }

        if (!isNew)
        {
            _tollStationManager.SetTitle(tollStation, input.Title);
            await _tollStationManager.SetProvineAsync(tollStation, input.ProvinceId);
            await _tollStationManager.SetCityAsync(tollStation, input.CityId);
            return Mapper.Map<TollStation, TollStationOutputDto>(await Repository.UpdateAsync(tollStation));
        }
        return Mapper.Map<TollStation, TollStationOutputDto>(await Repository.InsertAsync(tollStation));
    }
}