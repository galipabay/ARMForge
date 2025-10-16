using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverDto>> GetAllDriversAsync();
        Task<DriverDto?> GetDriverByIdAsync(int id);
        //Task<Driver> AddDriverAsync(Driver driver);
        Task<DriverDto?> UpdateDriverAsync(int id,DriverUpdateDto driver);
        Task<bool> DeleteDriverAsync(int id);
        Task<DriverDto> CreateDriverAsync(DriverCreateDto driver);
    }
}
