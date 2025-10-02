using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
using ARMForge.Kernel.Interfaces.UnitOfWork;
using ARMForge.Types.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IGenericRepository<Shipment> _shipmentRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Driver> _driverRepository;
        private readonly IGenericRepository<Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ShipmentService(IGenericRepository<Shipment> shipmentRepository,
            IGenericRepository<Order> orderRepository,
            IGenericRepository<Driver> driverRepository,
            IGenericRepository<Vehicle> vehicleRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _shipmentRepository = shipmentRepository;
            _orderRepository = orderRepository;
            _driverRepository = driverRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ShipmentDto> AddShipmentAsync(ShipmentCreateDto dto)
        {
            // 1. Veritabanından ilgili kayıtları çek
            var order = await _orderRepository.GetByIdAsync(dto.OrderId);
            var driver = await _driverRepository.GetByIdAsync(dto.DriverId);
            var vehicle = await _vehicleRepository.GetByIdAsync(dto.VehicleId);

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
            var shipment = new Shipment
            {
                OrderId = dto.OrderId,
                DriverId = dto.DriverId,
                VehicleId = dto.VehicleId,
                Origin = dto.Origin,
                Destination = dto.Destination,
                DepartureDate = dto.DepartureDate,
                EstimatedDeliveryDate = dto.EstimatedDeliveryDate,
                Status = "Kargoda",
                TrackingNumber = $"TRK-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
            };

            await _shipmentRepository.AddAsync(shipment);

            // 5. Driver ve Vehicle güncelle
            driver.IsAvailable = false;
            vehicle.IsAvailable = false;
            _driverRepository.Update(driver);
            _vehicleRepository.Update(vehicle);

            // 6. Order güncelle
            order.Status = "Kargoda";
            _orderRepository.Update(order);

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
        public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync()
        {
            return await _shipmentRepository.GetAllAsync();
        }
        public async Task<Shipment> GetShipmentByIdAsync(int id)
        {
            return await _shipmentRepository.GetByIdAsync(id);
        }

        public async Task<Shipment> UpdateShipmentAsync(Shipment shipment)
        {
            _shipmentRepository.Update(shipment);
            await _shipmentRepository.SaveChangesAsync();
            return shipment;
        }
    }
}
