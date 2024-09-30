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
    public class TaxExemptMapProfile : Profile
    {
        public TaxExemptMapProfile()
        {
            CreateMap<TaxExempt, TaxExemptOutputDto>()
                .ForMember(des => des.Province, opts => opts.MapFrom(src => src.Province != null
                ? new KeyValueDto<Guid>
                {
                    Id = src.Province.Id,
                    Name = src.Province.Name,
                } : null))
                .ForMember(des => des.City, opts => opts.MapFrom(src => src.City != null
                ? new KeyValueDto<Guid>
                {
                    Id = src.City.Id,
                    Name = src.City.Name,
                } : null))
                .ForMember(des => des.TollStation, opts => opts.MapFrom(src => src.TollStation != null
                ? new KeyValueDto<Guid>
                {
                    Id = src.TollStation.Id,
                    Name = src.TollStation.Title,
                } : null))
                .ForMember(des => des.VehicleCategory, opts => opts.MapFrom(src => src.VehicleCategory != null
                ? new KeyValueDto<Guid>
                {
                    Id = src.VehicleCategory.Id,
                    Name = src.VehicleCategory.Name,
                } : null))
                ;
        }
    }
}