using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces;
using ARMForge.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class ShipmentProcessService : IShipmentProcessService
    {
        private readonly IShipmentService _shipmentService;
        private readonly IOrderService _orderService;
        private readonly IDriverService _driverService;
        private readonly IVehicleService _vehicleService;
        private readonly IGenericRepository<Shipment> _shipmentRepository; // YENİ: İlişkileri kolayca yönetmek için eklendi

        public ShipmentProcessService(
            IShipmentService shipmentService,
            IOrderService orderService,
            IDriverService driverService,
            IVehicleService vehicleService,
            IGenericRepository<Shipment> shipmentRepository) // YENİ: IGenericRepository<Shipment> eklendi
        {
            _shipmentService = shipmentService;
            _orderService = orderService;
            _driverService = driverService;
            _vehicleService = vehicleService;
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Shipment> CreateShipmentAsync(CreateShipmentRequestDto request)
        {
            // 1. Gerekli varlıkları veritabanından getirir.
            var order = await _orderService.GetOrderByIdAsync(request.OrderId);
            var driver = await _driverService.GetDriverByIdAsync(request.DriverId);
            var vehicle = await _vehicleService.GetVehicleByIdAsync(request.VehicleId);

            // 2. Varlıkların varlığını ve durumlarını doğrular.
            if (order == null)
            {
                throw new ArgumentException("Sipariş bulunamadı.", nameof(request.OrderId));
            }
            if (driver == null || !driver.IsAvailable)
            {
                throw new ArgumentException("Sürücü bulunamadı veya müsait değil.", nameof(request.DriverId));
            }
            if (vehicle == null || !vehicle.IsAvailable)
            {
                throw new ArgumentException("Araç bulunamadı veya müsait değil.", nameof(request.VehicleId));
            }

            // 3. Yeni bir sevkiyat varlığı oluşturur.
            var newShipment = new Shipment
            {
                OrderId = request.OrderId,
                DriverId = request.DriverId,
                VehicleId = request.VehicleId,
                Status = "Kargoda",
                Origin = "Depo", // Örnek veri
                Destination = "Müşteri Adresi", // Örnek veri
                DepartureDate = DateTimeOffset.UtcNow,
                EstimatedDeliveryDate = DateTimeOffset.UtcNow.AddDays(3),
                TrackingNumber = Guid.NewGuid().ToString("N").Substring(0, 10) // Rastgele takip numarası
            };

            // 4. İlgili varlıkların durumunu günceller.
            driver.IsAvailable = false;
            vehicle.IsAvailable = false;
            order.Status = "Kargoda";

            // 5. Değişiklikleri veritabanına kaydeder.
            await _shipmentRepository.AddAsync(newShipment);
            _driverService.UpdateDriverAsync(driver); // Sürücüyü güncelle
            _vehicleService.UpdateVehicleAsync(vehicle); // Aracı güncelle
            _orderService.UpdateOrderAsync(order); // Siparişi güncelle

            await _shipmentRepository.SaveChangesAsync(); // Tüm değişiklikleri tek bir işlemde kaydeder

            return newShipment;
        }
    }
}
