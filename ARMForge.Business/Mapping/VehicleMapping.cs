using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using AutoMapper;
using System;

namespace ARMForge.Business.Mapping
{
    public class VehicleMapping : Profile
    {
        public VehicleMapping()
        {
            CreateMap<VehicleCreateDto, Vehicle>();

            CreateMap<VehicleUpdateDto, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Shipments, opt => opt.Ignore())
                .ForMember(dest => dest.PlateNumber, opt => opt.Condition(src => !string.IsNullOrEmpty(src.PlateNumber)))
                .ForMember(dest => dest.VehicleType, opt => opt.Condition(src => !string.IsNullOrEmpty(src.VehicleType)))
                .ForMember(dest => dest.Make, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Make)))
                .ForMember(dest => dest.Model, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Model)))
                .ForMember(dest => dest.CapacityKg, opt => opt.Condition(src => src.CapacityKg.HasValue && src.CapacityKg.Value > 0))
                .ForMember(dest => dest.CapacityM3, opt => opt.Condition(src => src.CapacityM3.HasValue && src.CapacityM3.Value > 0))
                .ForMember(dest => dest.IsAvailable, opt => opt.Condition(src => src.IsAvailable.HasValue))
                .AfterMap((src, dest) => dest.UpdatedAt = DateTime.UtcNow);

            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.ShipmentCount, opt => opt.MapFrom(src => src.Shipments.Count));
        }
    }
}
