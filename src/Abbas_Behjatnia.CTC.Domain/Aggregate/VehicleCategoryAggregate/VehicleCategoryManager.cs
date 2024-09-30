
using System.ComponentModel.DataAnnotations;
using Abbas_Behjatnia.Shared.AspNetCore;
using Abbas_Behjatnia.Shared.Domain.Repositories;
using Abbas_Behjatnia.Shared.Domain.Services;

namespace Abbas_Behjatnia.CTC.Domain.Aggregates;

public class VehicleCategoryManager : DomainService<VehicleCategory>
{
    public IRepository<VehicleCategory> _vehicleCategoryRepository => LazyServiceProvider.LazyGetService<IRepository<VehicleCategory>>();

    public VehicleCategory New(string name)
    {
        var id = new Guid();
        ChecName(name);
        var vehicleCategory = new VehicleCategory(id, name);
        return vehicleCategory;
    }

    public void ChecName(string name)
    {
        if (name is null || string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException($"The Vehicle Category Name could not be null or empty!!");
        }
    }

    public void SetName(VehicleCategory vehicleCategory, string name)
    {
        ChecName(name);
        vehicleCategory.Name = name;
    }

    public async Task SetParentAsync(VehicleCategory vehicleCategory, Guid parentId)
    {
        if (parentId == default)
            return;

        var parent = await _vehicleCategoryRepository.FindAsync(parentId);
        if (parent != null)
        {
            throw new ValidationException($"The desired Vehicle Category does not exist and could not be set as a Parent!!");
        }

        vehicleCategory.ParentId = parentId;
        vehicleCategory.Parent = parent;

    }
}
