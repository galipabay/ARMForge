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
        Task<ShipmentDto> AddShipmentAsync(ShipmentCreateDto shipment);
        Task<bool> DeleteShipmentAsync(int id);
        Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync();
        Task<ShipmentDto?> GetShipmentByIdAsync(int id);
        Task<ShipmentDto?> UpdateShipmentAsync(int id, ShipmentUpdateDto shipmentUpdateDto);
    }
}
