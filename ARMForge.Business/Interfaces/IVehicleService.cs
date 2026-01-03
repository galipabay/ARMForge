using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
        Task<VehicleDto?> GetVehicleByIdAsync(int id);
        Task<VehicleDto> AddVehicleAsync(VehicleCreateDto vehicleDto);
        Task<VehicleDto?> UpdateVehicleAsync(int id, VehicleUpdateDto vehicleDto);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
