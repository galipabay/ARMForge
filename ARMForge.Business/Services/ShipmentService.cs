using ARMForge.Business.Interfaces;
using ARMForge.Infrastructure;
using ARMForge.Kernel.Entities;
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
        private readonly IGenericRepository<Shipment> _shipmentRepository; // YENİ: İlişkileri kolayca yönetmek için eklendi
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ShipmentService(
            IOrderService orderService,
            IDriverService driverService,
            IVehicleService vehicleService,
            IGenericRepository<Shipment> shipmentRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork) // YENİ: IGenericRepository<Shipment> eklendi
        {
            _orderService = orderService;
            _driverService = driverService;
            _vehicleService = vehicleService;
            _shipmentRepository = shipmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ShipmentDto> AddShipmentAsync(ShipmentCreateDto dto)
        {
            // 1. Gerekli varlıkları veritabanından getirir.
            var order = await _orderService.GetOrderByIdAsync(dto.OrderId);
            var driver = await _driverService.GetDriverByIdAsync(dto.DriverId);
            var vehicle = await _vehicleService.GetVehicleByIdAsync(dto.VehicleId);

            // 2. Kayıtları kontrol et
            if (order == null)
                throw new InvalidOperationException("Order bulunamadı.");
            if (driver == null)
                throw new InvalidOperationException("Driver bulunamadı.");
            if (vehicle == null)
                throw new InvalidOperationException("Vehicle bulunamadı.");

            // 3. Müsaitlik kontrolü
            if (!driver.IsAvailable)
                throw new InvalidOperationException("Seçilen sürücü müsait değil.");
            if (!vehicle.IsAvailable)
                throw new InvalidOperationException("Seçilen araç müsait değil.");

            // 4. Shipment oluştur
            var shipment = _mapper.Map<Shipment>(dto);
            shipment.Status = "Kargoda";
            shipment.TrackingNumber = $"TRK-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            // 5. İlgili varlıkların durumunu günceller.
            driver.IsAvailable = false;
            driver.IsOnDuty = true;

            vehicle.IsAvailable = false;

            order.Status = "Kargoda";

            var driverUpdateDto = new DriverUpdateDto
            {
                IsOnDuty = driver.IsOnDuty,
                IsAvailable = driver.IsAvailable
            };

            // 6. Değişiklikleri veritabanına kaydeder.
            await _shipmentRepository.AddAsync(shipment);
            // Paralel update (daha performanslı)
            await Task.WhenAll(
                _driverService.UpdateDriverAsync(driver.Id, driverUpdateDto)
                //_vehicleService.UpdateVehicleAsync(vehicle),
                //_orderService.UpdateOrderAsync(order)
            );

            // 7. Transaction commit
            await _unitOfWork.CommitAsync();

            // 8. DTO olarak dön
            return _mapper.Map<ShipmentDto>(shipment);
        }
        public async Task<bool> DeleteShipmentAsync(int id)
        {
            var shipmentToDelete = await _shipmentRepository.GetByIdAsync(id);
            if (shipmentToDelete == null) return false;

            _shipmentRepository.Delete(shipmentToDelete);
            return await _shipmentRepository.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync()
        {
            var shipments = await _shipmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
        }

        public async Task<ShipmentDto?> GetShipmentByIdAsync(int id)
        {
            var shipment = await _shipmentRepository.GetByConditionAsync(
                s => s.Id == id,
                include: q => q
                    .Include(s => s.Driver)
                    .Include(s => s.Vehicle)
                    .Include(s => s.Order)
            );

            return shipment == null ? null : _mapper.Map<ShipmentDto>(shipment);
        }

        public async Task<ShipmentDto?> UpdateShipmentAsync(int id, ShipmentUpdateDto dto)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(id);
            if (shipment == null) return null;

            // AutoMapper ile DTO’dan entity’ye güncelle
            _mapper.Map(dto, shipment);
            shipment.UpdatedAt = DateTime.UtcNow;

            _shipmentRepository.Update(shipment);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ShipmentDto>(shipment);
        }
    }
}
