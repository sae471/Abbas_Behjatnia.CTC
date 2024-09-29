using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abbas_Behjatnia.CTC.Application.Contracts;
using Abbas_Behjatnia.CTC.Domain.Aggregates;
using Abbas_Behjatnia.Shared.Application.Dto;
using AutoMapper;

namespace Abbas_Behjatnia.CTC.Application.Services
{
    public class VehicleCategoryMapProfile : Profile
    {
        public VehicleCategoryMapProfile()
        {
            CreateMap<VehicleCategory, VehicleCategoryOutputDto>()
                .ForMember(des => des.Parent, opts => opts.MapFrom(src => src.Parent != null
                ? new KeyValueDto<Guid>
                {
                    Id = src.Parent.Id,
                    Name = src.Parent.Name,
                } : null));
        }
    }
}