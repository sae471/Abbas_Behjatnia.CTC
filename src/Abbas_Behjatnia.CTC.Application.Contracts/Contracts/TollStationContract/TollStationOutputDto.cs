using System;

using Abbas_Behjatnia.CTC.Domain.Shared;
using Abbas_Behjatnia.Shared.Application.Dto;

namespace Abbas_Behjatnia.CTC.Application.Contracts;
public class TollStationOutputDto : EntityDto<Guid>
{
    public required string Title { get; set; }
    public required KeyValueDto<Guid> Province { get; set; }
    public required KeyValueDto<Guid> City { get; set; }
}