

using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class VehicleManager : DomainService<Vehicle>
{
    IRepository<Vehicle> _vehicleRepository => LazyServiceProvider.LazyGetService<IRepository<Vehicle>>();
    IRepository<VehicleCategory> _vehicleCategoryRepository => LazyServiceProvider.LazyGetService<IRepository<VehicleCategory>>();

    public async Task<Vehicle> NewAsync(VehicleType type, VehicleOwnerShipType ownerShipType, string plateNumber, string chassisNumber)
    {
        var id = new Guid();
        await CheckPlateAndChassisNumberAsync(plateNumber, chassisNumber);
        CheckVehicleType(type);
        CheckVehicleOwnerShipType(ownerShipType);
        var vehicle = new Vehicle(id, type, ownerShipType, plateNumber, chassisNumber);
        return vehicle;
    }

    public void CheckVehicleType(VehicleType type)
    {
        if (!Enum.IsDefined(typeof(VehicleType), type))
        {
            throw new ValidationException($"Vehicle Type is invalid!!");
        }
    }

    public void SetVehicleType(Vehicle vehicle, VehicleType type)
    {
        CheckVehicleType(type);
        vehicle.Type = type;
    }

    public void CheckVehicleOwnerShipType(VehicleOwnerShipType ownerShipType)
    {
        if (!Enum.IsDefined(typeof(VehicleOwnerShipType), ownerShipType))
        {
            throw new ValidationException($"Vehicle ownerShip type is invalid!!");
        }
    }

    public void SetVehicleOwnerShip(Vehicle vehicle, VehicleOwnerShipType ownerShipType)
    {
        CheckVehicleOwnerShipType(ownerShipType);
        vehicle.OwnerShipType = ownerShipType;
    }

    public async Task CheckPlateAndChassisNumberAsync(string plateNumber, string chassisNumber, Guid vehicleId = default)
    {
        if (chassisNumber is null || string.IsNullOrWhiteSpace(chassisNumber))
        {
            throw new ValidationException($"Chassis Number could not be null or empty!!");
        }
        if (plateNumber is null || string.IsNullOrWhiteSpace(plateNumber))
        {
            throw new ValidationException($"Plate Number could not be null or empty!!");
        }
        var existedVehicle = await _vehicleRepository.FindAsync(it =>
            it.PlateNumber.ToLower().Trim().Equals(plateNumber.ToLower().Trim()) &&
            it.ChassisNumber.ToLower().Trim().Equals(chassisNumber.ToLower().Trim()) &&
            (vehicleId == default || it.Id != vehicleId));

        if (existedVehicle != null)
        {
            throw new ValidationException($"The vehicle with the entered Chassis Number and Plate Number exists!!");
        }
    }

    public async Task SetPlateAndChassisNumberAsync(Vehicle vehicle, string chassisNumber, string plateNumber)
    {
        await CheckPlateAndChassisNumberAsync(plateNumber, chassisNumber, vehicle.Id);
        vehicle.PlateNumber = plateNumber;
        vehicle.ChassisNumber = chassisNumber;
    }

    public async Task SetVehicleCategoryAsync(Vehicle vehicle, Guid vehicleCategoryId)
    {
        if (vehicleCategoryId == default)
            return;

        var vehicleCategory = await _vehicleCategoryRepository.FindAsync(vehicleCategoryId);
        if (vehicleCategory == null)
        {
            throw new ValidationException($"The desired Vehicle Category does not exist!!");
        }

        vehicle.VehicleCategoryId = vehicleCategoryId;
        vehicle.VehicleCategory = vehicleCategory;

    }
}
