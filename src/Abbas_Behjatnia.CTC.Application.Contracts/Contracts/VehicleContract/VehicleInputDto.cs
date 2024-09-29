
using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class VehicleInputDto : EntityDto<Guid>
{
    public required VehicleType Type { get; set; }
    public required VehicleOwnerShipType OwnerShipType { get; set; }
    public required string PlateNumber { get; set; }
    public required string ChassisNumber { get; set; }
    public string? ManufacturerCompany { get; set; }
    public string? ManufacturerClass { get; set; }
    public string? Color { get; set; }
    public string? YearofManufacture { get; set; }
    public Guid? VehicleCategoryId { get; set; }
}