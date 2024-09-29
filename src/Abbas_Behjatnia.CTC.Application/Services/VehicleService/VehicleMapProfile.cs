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
    public class VehicleMapProfile : Profile
    {
        public VehicleMapProfile()
        {
            CreateMap<Vehicle, VehicleOutputDto>()
                .ForMember(des => des.VehicleCategory, opts => opts.MapFrom(src => src.VehicleCategory != null
                ? new KeyValueDto<Guid>
                {
                    Id = src.VehicleCategory.Id,
                    Name = src.VehicleCategory.Name,
                } : null));
        }
    }
}