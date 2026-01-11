using ARMForge.Business.Interfaces;
using ARMForge.Infrastructure;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Enums;
using ARMForge.Kernel.Interfaces.GenericRepository;
using ARMForge.Kernel.Interfaces.UnitOfWork;
using ARMForge.Types.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IOrderService _orderService;
        private readonly IDriverService _driverService;
        private readonly IVehicleService _vehicleService;
        private readonly IGenericRepository<Shipment> _shipmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShipmentService(
            IOrderService orderService,
            IDriverService driverService,
            IVehicleService vehicleService,
            IGenericRepository<Shipment> shipmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _driverService = driverService;
            _vehicleService = vehicleService;
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // 📌 ADD
        public async Task<ShipmentDto> AddShipmentAsync(ShipmentCreateDto dto)
        {
            var order = await _orderService.GetOrderByIdAsync(dto.OrderId);
            var driver = await _driverService.GetDriverByIdAsync(dto.DriverId);
            var vehicle = await _vehicleService.GetVehicleByIdAsync(dto.VehicleId);

            if (order == null)
                throw new InvalidOperationException("Sipariş bulunamadı.");
            if (driver == null)
                throw new InvalidOperationException("Sürücü bulunamadı.");
            if (vehicle == null)
                throw new InvalidOperationException("Araç bulunamadı.");

            if (!driver.IsAvailable)
                throw new InvalidOperationException("Seçilen sürücü müsait değil.");
            if (!vehicle.IsAvailable)
                throw new InvalidOperationException("Seçilen araç müsait değil.");

            var shipment = _mapper.Map<Shipment>(dto);
            shipment.Status = ShipmentStatus.InTransit; // Enum kullanımı
            shipment.TrackingNumber = $"TRK-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            // Sürücü ve araç durumları
            driver.IsAvailable = false;
            driver.IsOnDuty = true;
            vehicle.IsAvailable = false;

            // Sipariş durumu
            order.Status = OrderStatus.Shipped;

            // DB işlemleri
            await _shipmentRepository.AddAsync(shipment);
            await Task.WhenAll(
                _driverService.UpdateDriverAsync(driver.Id, new DriverUpdateDto
                {
                    IsAvailable = driver.IsAvailable,
                    IsOnDuty = driver.IsOnDuty
                }),
                _vehicleService.UpdateVehicleAsync(vehicle.Id, new VehicleUpdateDto
                {
                    IsAvailable = vehicle.IsAvailable
                }),
                _orderService.UpdateOrderAsync(order.Id, new OrderUpdateDto
                {
                    Status = order.Status
                })
            );

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ShipmentDto>(shipment);
        }

        // 📌 UPDATE
        public async Task<ShipmentDto?> UpdateShipmentAsync(int id, ShipmentUpdateDto dto)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(id);
            if (shipment == null) return null;

            // Partial update mantığı
            _mapper.Map(dto, shipment);
            shipment.UpdatedAt = DateTime.UtcNow;

            _shipmentRepository.Update(shipment);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ShipmentDto>(shipment);
        }

        // 📌 DELETE
        public async Task<bool> DeleteShipmentAsync(int id)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(id);
            if (shipment == null) return false;

            // Sürücü ve araç müsait hale gelir
            var driver = await _driverService.GetDriverByIdAsync(shipment.DriverId);
            var vehicle = await _vehicleService.GetVehicleByIdAsync(shipment.VehicleId);

            if (driver != null)
            {
                driver.IsAvailable = true;
                driver.IsOnDuty = false;
                await _driverService.UpdateDriverAsync(driver.Id, new DriverUpdateDto
                {
                    IsAvailable = driver.IsAvailable,
                    IsOnDuty = driver.IsOnDuty
                });
            }

            if (vehicle != null)
            {
                vehicle.IsAvailable = true;
                await _vehicleService.UpdateVehicleAsync(vehicle.Id, new VehicleUpdateDto
                {
                    IsAvailable = vehicle.IsAvailable
                });
            }

            _shipmentRepository.Delete(shipment);
            await _unitOfWork.CommitAsync();

            return true;
        }

        // 📌 GET ALL
        public async Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync()
        {
            var shipments = await _shipmentRepository.GetAllWithIncludesAsync(
                s => s.Order,
                s => s.Driver,
                s => s.Vehicle
            );

            return _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
        }

        // 📌 GET BY ID
        public async Task<ShipmentDto?> GetShipmentByIdAsync(int id)
        {
            var shipment = await _shipmentRepository.GetByConditionAsync(
                s => s.Id == id,
                include: q => q
                    .Include(s => s.Order)
                    .Include(s => s.Driver)
                    .Include(s => s.Vehicle)
            );

            return shipment == null ? null : _mapper.Map<ShipmentDto>(shipment);
        }
    }
}
