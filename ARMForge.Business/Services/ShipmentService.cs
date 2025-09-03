using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces;
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

        public ShipmentService(IGenericRepository<Shipment> shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Shipment> AddShipmentAsync(Shipment shipment)
        {
            await _shipmentRepository.AddAsync(shipment);
            await _shipmentRepository.SaveChangesAsync();
            return shipment;

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
