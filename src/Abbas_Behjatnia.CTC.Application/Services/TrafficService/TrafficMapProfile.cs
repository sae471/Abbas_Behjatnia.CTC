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
    public class TrafficMapProfile : Profile
    {
        public TrafficMapProfile()
        {
            CreateMap<Traffic, TrafficOutputDto>()
                .ForMember(des => des.Vehicle, opts => opts.MapFrom(src => src.Vehicle != null
                ? new KeyValueDto<Guid>
                {
                    Id = src.Vehicle.Id,
                    Name = src.Vehicle.PlateNumber,
                } : null))
                .ForMember(des => des.TollStation, opts => opts.MapFrom(src => src.TollStation != null
                ? new KeyValueDto<Guid>
                {
                    Id = src.TollStation.Id,
                    Name = src.TollStation.Title,
                } : null))
                ;
        }
    }
}