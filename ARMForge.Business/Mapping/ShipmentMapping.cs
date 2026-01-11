using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using AutoMapper;
using System;

namespace ARMForge.Business.Mapping
{
    public class ShipmentMapping : Profile
    {
        public ShipmentMapping()
        {
            CreateMap<Shipment, ShipmentDto>();

            CreateMap<ShipmentCreateDto, Shipment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<ShipmentUpdateDto, Shipment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt =>
                {
                    opt.Condition((src, dest, srcMember) =>
                    {
                        if (srcMember == null) return false;
                        if (srcMember is string s) return !string.IsNullOrWhiteSpace(s);
                        return true;
                    });
                });
        }
    }
}
