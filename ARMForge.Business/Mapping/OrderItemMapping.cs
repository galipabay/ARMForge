using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using AutoMapper;

namespace ARMForge.Business.Mapping
{
    public class OrderItemMapping : Profile
    {
        public OrderItemMapping()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<OrderItemCreateDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Weight, opt => opt.Ignore())
                .ForMember(dest => dest.Volume, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<OrderItemUpdateDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Weight, opt => opt.Ignore())
                .ForMember(dest => dest.Volume, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
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
