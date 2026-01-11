using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync();
        Task<ShipmentDto?> GetShipmentByIdAsync(int id);
        Task<ShipmentDto> AddShipmentAsync(ShipmentCreateDto shipment);
        Task<ShipmentDto?> UpdateShipmentAsync(int id, ShipmentUpdateDto shipmentUpdateDto);
        Task<bool> DeleteShipmentAsync(int id);
    }
}
