using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllDriversAsync();
        Task<Driver> GetDriverByIdAsync(int id);
        Task<Driver> AddDriverAsync(Driver driver);
        Task<Driver> UpdateDriverAsync(Driver driver);
        Task<bool> DeleteDriverAsync(int id);
    }
}
