using System;

using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class TrafficOutputDto : EntityDto<Guid>
{
    public required DateTime DateTime { get; set; }
    public required KeyValueDto<Guid> Vehicle { get; set; }
    public required KeyValueDto<Guid> TollStation { get; set; }
}