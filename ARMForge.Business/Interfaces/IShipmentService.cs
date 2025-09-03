using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
        Task<Shipment> GetShipmentByIdAsync(int id);
        Task<Shipment> AddShipmentAsync(Shipment shipment);
        Task<Shipment> UpdateShipmentAsync(Shipment shipment);
        Task<bool> DeleteShipmentAsync(int id);
    }
}
