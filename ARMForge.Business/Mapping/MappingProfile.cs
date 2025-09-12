using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer entity'sini CustomerDto'ya dönüştür
            CreateMap<Customer, CustomerDto>();

            // Order entity'sini OrderDto'ya dönüştür
            CreateMap<Order, OrderDto>();
        }
    }
}
