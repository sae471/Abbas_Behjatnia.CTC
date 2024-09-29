
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Application.Contracts.Contracts.VehicleContract;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class VehicleAppService : BaseAppService<Vehicle, VehicleOutputDto, VehicleInputDto>, IVehicleAppService
{
    private VehicleManager _vehicleManager => (VehicleManager)LazyServiceProvider.LazyGetService<IDomainService<Vehicle>>();
    public override async Task<VehicleOutputDto> UpsertAsync(VehicleInputDto input)
    {
        var isNew = false;
        var vehicle = await Repository.FindAsync(input.Id);
        if (vehicle == null)
        {
            isNew = true;
            vehicle = await _vehicleManager.NewAsync(input.Type, input.OwnerShipType, input.PlateNumber, input.ChassisNumber);
        }
        vehicle.ManufacturerCompany = input.ManufacturerCompany ?? string.Empty;
        vehicle.ManufacturerClass = input.ManufacturerClass ?? string.Empty;
        vehicle.YearofManufacture = input.YearofManufacture ?? string.Empty;
        vehicle.Color = input.Color ?? string.Empty;
        await _vehicleManager.SetVehicleCategoryAsync(vehicle, input.VehicleCategoryId ?? default);

        if (!isNew)
        {
            _vehicleManager.SetVehicleType(vehicle, input.Type);
            _vehicleManager.SetVehicleOwnerShip(vehicle, input.OwnerShipType);
            await _vehicleManager.SetPlateAndChassisNumberAsync(vehicle, input.ChassisNumber, input.PlateNumber);
            return Mapper.Map<Vehicle, VehicleOutputDto>(await Repository.UpdateAsync(vehicle));
        }
        return Mapper.Map<Vehicle, VehicleOutputDto>(await Repository.InsertAsync(vehicle));
    }
}