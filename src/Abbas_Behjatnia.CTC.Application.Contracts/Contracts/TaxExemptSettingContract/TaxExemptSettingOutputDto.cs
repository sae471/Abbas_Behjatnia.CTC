using System;

using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class TaxExemptSettingOutputDto : EntityDto<Guid>
{
    public TaxExemptSettingType Type { get; set; }
    public required string Value { get; set; }
}