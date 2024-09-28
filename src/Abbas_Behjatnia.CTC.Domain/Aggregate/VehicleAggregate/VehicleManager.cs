

using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class VehicleManager : DomainService<Vehicle>
{
    public IRepository<Vehicle> _vehicleRepository => LazyServiceProvider.LazyGetService<IRepository<Vehicle>>();

    public async Task<Vehicle> NewAsync(string plateNumber, VehicleType type)
    {
        var id = new Guid();
        await CheckPlateNumberAsync(plateNumber);
        CheckVehicleType(type);
        var vehicle = new Vehicle(id, plateNumber, type);
        return vehicle;
    }

    public void CheckVehicleType(VehicleType type)
    {
        if (!Enum.IsDefined(typeof(VehicleType), type))
        {
            throw new ValidationException($"Vehicle type is invalid!!");
        }
    }

    public async Task CheckPlateNumberAsync(string plateNumber, Guid vehicleId = default)
    {
        if (plateNumber is null || String.IsNullOrWhiteSpace(plateNumber.ToString()))
        {
            throw new ValidationException($"Plate number Could not be null or empty!!");
        }
        var existedVehicle = await _vehicleRepository.FirstOrDefaultAsync(it =>
            it.PlateNumber.ToLower().Trim().Equals(plateNumber.ToLower().Trim()) &&
            (vehicleId == default || it.Id != vehicleId));

        if (existedVehicle != null)
        {
            throw new ValidationException($"The entered plate number exists!!");
        }
    }
    public async Task SetVehiclePlateNumber(Vehicle vehicle, string plateNumber)
    {
        await CheckPlateNumberAsync(plateNumber, vehicle.Id);
        vehicle.PlateNumber = plateNumber;
    }
}
