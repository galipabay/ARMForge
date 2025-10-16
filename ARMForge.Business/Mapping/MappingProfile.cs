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
            #region Customer
            // Customer entity'sini CustomerDto'ya dönüştür
            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerUpdateDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id’yi koru 
            #endregion

            #region Order
            // Order entity'sini OrderDto'ya dönüştür
            CreateMap<Order, OrderDto>();

            // İşte bu satır eksik veya yanlış olabilir!
            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore()); // Müşteri navigasyon özelliğini Ignore et 
            #endregion

            #region Driver
            CreateMap<DriverCreateDto, Driver>()
                    .ForMember(dest => dest.User, opt => opt.Ignore()); // Müşteri navigasyon özelliğini Ignore et

            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.UserFullName, opt => opt
                .MapFrom(src => src.User != null ? src.User.Firstname + " " + src.User.Lastname : null));

            CreateMap<DriverDto, Driver>();
            #endregion

            #region Vehicle
            CreateMap<VehicleCreateDto, Vehicle>();

            CreateMap<Vehicle, VehicleDto>();
            #endregion

            #region Shipment
            CreateMap<ShipmentCreateDto, Shipment>();
            CreateMap<Shipment, ShipmentDto>()
                .ForMember(dest => dest.DriverFullName, opt => opt.MapFrom(src =>
                    src.Driver != null && src.Driver.User != null
                        ? $"{src.Driver.User.Firstname} {src.Driver.User.Lastname}"
                        : string.Empty))
                .ForMember(dest => dest.VehiclePlate, opt => opt.MapFrom(src =>
                    src.Vehicle != null ? src.Vehicle.PlateNumber : string.Empty))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src =>
                    src.Order != null ? src.Order.OrderNumber : string.Empty));
            // 🔥 ShipmentUpdateDto → Shipment
            CreateMap<ShipmentUpdateDto, Shipment>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            // null gönderilen alanlar güncellenmez
            #endregion
        }
    }
}
