using System;


using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class TrafficManager : DomainService<Traffic>
{
    IRepository<Vehicle> _vehicleRepository => LazyServiceProvider.LazyGetService<IRepository<Vehicle>>();
    IRepository<TollStation> _tollStationRepository => LazyServiceProvider.LazyGetService<IRepository<TollStation>>();

    public async Task<Traffic> NewAsync(DateTime time, Guid vehicleId, Guid tollStationId)
    {
        var id = new Guid();
        CheckDateTime(time);
        await CheckVehicleAsync(vehicleId);
        await CheckTollStationAsync(tollStationId);
        var traffic = new Traffic(id, time,vehicleId,tollStationId);
        return traffic;
    }

    public void CheckDateTime(DateTime dateTime)
    {
        if (dateTime == DateTime.MinValue)
        {
            throw new ValidationException($"Traffic Time could not be null or empty!!");
        }
    }

    public void SetDateTime(Traffic traffic, DateTime dateTime)
    {
        CheckDateTime(dateTime);
        traffic.DateTime = dateTime;
    }

    public async Task<Vehicle> CheckVehicleAsync(Guid vehicleId)
    {
        if (vehicleId == default)
        {
            throw new ValidationException($"The Vehicle could not be null or empty!!");
        }
        var vehicle = await _vehicleRepository.FindAsync(it => it.Id == vehicleId);
        if (vehicle == null)
        {
            throw new ValidationException($"The desired Vehicle does not exist!!");
        }

        return vehicle;
    }
    public async Task SetVehicleAsync(Traffic traffic, Guid vehicleId)
    {
        var vehicle = await CheckVehicleAsync(vehicleId);

        traffic.VehicleId = vehicleId;
        traffic.Vehicle = vehicle;
    }

    public async Task<TollStation> CheckTollStationAsync(Guid tollStationId)
    {
        if (tollStationId == default)
        {
            throw new ValidationException($"The Toll Station could not be null or empty!!");
        }
        var tollStation = await _tollStationRepository.FindAsync(it => it.Id == tollStationId);
        if (tollStation == null)
        {
            throw new ValidationException($"The desired Toll Station does not exist!!");
        }

        return tollStation;
    }
    public async Task SetTollStationsync(Traffic traffic, Guid tollStationId)
    {
        var tollStation = await CheckTollStationAsync(tollStationId);

        traffic.TollStationId = tollStationId;
        traffic.TollStation = tollStation;
    }
}
