using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using AutoMapper;

namespace ARMForge.Business.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductCreateDto, Product>();

            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UnitPrice,
                    opt => opt.PreCondition(src => src.UnitPrice.HasValue))
                .ForMember(dest => dest.StockQuantity,
                    opt => opt.PreCondition(src => src.StockQuantity.HasValue))
                .ForMember(dest => dest.Name,
                    opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Name)))
                .ForMember(dest => dest.StockKeepingUnit,
                    opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.StockKeepingUnit)))
                .ForMember(dest => dest.Description,
                    opt => opt.PreCondition(src => src.Description != null))
                .ForMember(dest => dest.Category,
                    opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Category)));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.OrderItemCount,
                    opt => opt.MapFrom(src => src.OrderItems.Count));
        }
    }
}
