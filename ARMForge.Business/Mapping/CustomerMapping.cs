using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using AutoMapper;
using System;

namespace ARMForge.Business.Mapping
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.OrderCount, opt => opt.MapFrom(src => src.Orders.Count));
        }
    }
}
