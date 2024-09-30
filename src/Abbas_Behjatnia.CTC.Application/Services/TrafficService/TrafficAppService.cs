
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class TrafficAppService : BaseAppService<Traffic, TrafficOutputDto, TrafficInputDto>, ITrafficAppService
{
    private TrafficManager _trafficManager => (TrafficManager)LazyServiceProvider.LazyGetService<IDomainService<Traffic>>();
    public override async Task<TrafficOutputDto> UpsertAsync(TrafficInputDto input)
    {
        var isNew = false;
        var traffic = await Repository.FindAsync(input.Id);
        if (traffic == null)
        {
            isNew = true;
            traffic = await _trafficManager.NewAsync(input.DateTime, input.VehicleId, input.TollStationId);
        }

        if (!isNew)
        {
            _trafficManager.SetDateTime(traffic, input.DateTime);
            await _trafficManager.SetVehicleAsync(traffic, input.VehicleId);
            await _trafficManager.SetTollStationsync(traffic, input.TollStationId);
            return Mapper.Map<Traffic, TrafficOutputDto>(await Repository.UpdateAsync(traffic));
        }
        return Mapper.Map<Traffic, TrafficOutputDto>(await Repository.InsertAsync(traffic));
    }
}