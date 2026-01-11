using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using AutoMapper;

namespace ARMForge.Business.Mapping
{
    public class DriverMapping : Profile
    {
        public DriverMapping()
        {
            CreateMap<DriverCreateDto, Driver>()
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<DriverUpdateDto, Driver>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.UserFullName, opt => opt
                    .MapFrom(src => src.User != null ? src.User.Firstname + " " + src.User.Lastname : null))
                .ForMember(dest => dest.ShipmentCount,
                    opt => opt.MapFrom(src => src.Shipments != null ? src.Shipments.Count : 0));


        }
    }
}
