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
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id’yi koru

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.OrderCount, opt => opt.MapFrom(src => src.Orders.Count));
            #endregion

            #region Order
            CreateMap<OrderCreateDto, Order>();

            CreateMap<OrderUpdateDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Condition(src => src.CustomerId.HasValue))
                .ForMember(dest => dest.OrderDate, opt => opt.Condition(src => src.OrderDate.HasValue))
                .ForMember(dest => dest.RequiredDate, opt => opt.Condition(src => src.RequiredDate.HasValue))
                .ForMember(dest => dest.ShippedDate, opt => opt.Condition(src => src.ShippedDate.HasValue))
                .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)))
                .ForMember(dest => dest.TotalAmount, opt => opt.Condition(src => src.TotalAmount.HasValue))
                .ForMember(dest => dest.OrderNumber, opt => opt.Condition(src => !string.IsNullOrEmpty(src.OrderNumber)))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Shipments, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentMethod, opt => opt.Condition(src => !string.IsNullOrEmpty(src.PaymentMethod)))
                .ForMember(dest => dest.DeliveryAddress, opt => opt.Condition(src => !string.IsNullOrEmpty(src.DeliveryAddress)));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerCompanyName, opt => opt.MapFrom(src => src.Customer.CompanyName))
                .ForMember(dest => dest.Shipments, opt => opt.MapFrom(src => src.Shipments)); // ✅ Shipment listesi
            #endregion

            #region OrderItem
            // CREATE
            CreateMap<OrderItemCreateDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.UnitPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Weight, opt => opt.Ignore())
                .ForMember(dest => dest.Volume, opt => opt.Ignore());
            // UPDATE (PATCH mantığı)
            CreateMap<OrderItemUpdateDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.UnitPrice, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));
            // READ
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.StockKeepingUnit,
                    opt => opt.MapFrom(src => src.Product.StockKeepingUnit))
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.Product.Category));
            #endregion

            #region Driver
            CreateMap<DriverCreateDto, Driver>()
                .ForMember(dest => dest.User, opt => opt.Ignore()); // ✅ DOĞRU

            CreateMap<DriverUpdateDto, Driver>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ✅ ID koru
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // ✅ UserId koru  
                .ForMember(dest => dest.User, opt => opt.Ignore()); // ✅ User ignore

            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.UserFullName, opt => opt
                    .MapFrom(src => src.User != null ? src.User.Firstname + " " + src.User.Lastname : null))
                .ForMember(dest => dest.ShipmentCount, opt => opt.MapFrom(src => src.Shipments.Count));
            #endregion

            #region Vehicle
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

            CreateMap<ShipmentUpdateDto, Shipment>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Shipment, ShipmentInfoDto>();
            #endregion

            #region Product
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
            #endregion
        }
    }
}
