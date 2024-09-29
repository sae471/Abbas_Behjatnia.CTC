using System;

using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class VehicleCategoryOutputDto : EntityDto<Guid>
{
    public string? Name { get; set; }
    public KeyValueDto<Guid>? Parent { get; set; }
}