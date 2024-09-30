using System;

using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class VehicleOutputDto : EntityDto<Guid>
{
    public VehicleType Type { get; set; }
    public VehicleOwnerShipType OwnerShipType { get; set; }
    public string? PlateNumber { get; set; }
    public string? ChassisNumber { get; set; }
    public string? ManufacturerCompany { get; set; }
    public string? ManufacturerClass { get; set; }
    public int? YearofManufacture { get; set; }
    public KeyValueDto<Guid> VehicleCategory { get; set; }
    public string? Color { get; set; }
}