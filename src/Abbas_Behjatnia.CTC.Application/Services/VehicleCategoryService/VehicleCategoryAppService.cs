
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Application.Contracts.Contracts.VehicleContract;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Services;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Application.Services;
public class VehicleCategoryAppService : BaseAppService<VehicleCategory, VehicleCategoryOutputDto, VehicleCategoryInputDto>, IVehicleCategoryAppService
{
    private VehicleCategoryManager _vehicleCategoryManager => (VehicleCategoryManager)LazyServiceProvider.LazyGetService<IDomainService<VehicleCategory>>();
    public override async Task<VehicleCategoryOutputDto> UpsertAsync(VehicleCategoryInputDto input)
    {
        var isNew = false;
        var vehicleCategory = await Repository.FindAsync(input.Id);
        if (vehicleCategory == null)
        {
            isNew = true;
            vehicleCategory = _vehicleCategoryManager.New(input.Name);
        }
        await _vehicleCategoryManager.SetParentAsync(vehicleCategory, input.ParentId ?? default);

        if (!isNew)
        {
            _vehicleCategoryManager.SetName(vehicleCategory, input.Name);
            return Mapper.Map<VehicleCategory, VehicleCategoryOutputDto>(await Repository.UpdateAsync(vehicleCategory));
        }
        return Mapper.Map<VehicleCategory, VehicleCategoryOutputDto>(await Repository.InsertAsync(vehicleCategory));
    }
}