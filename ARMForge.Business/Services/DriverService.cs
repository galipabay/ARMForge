using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class DriverService : IDriverService
    {
        private readonly IGenericRepository<Driver> _driverRepository;

        public DriverService(IGenericRepository<Driver> driverRepository)
        {
            _driverRepository = driverRepository;
        }
        public async Task<Driver> AddDriverAsync(Driver driver)
        {
            await _driverRepository.AddAsync(driver);
            await _driverRepository.SaveChangesAsync();
            return driver; // Eklenen sürücüyü döndür
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            var driverToDelete = await _driverRepository.GetByIdAsync(id);
            if (driverToDelete == null)
            {
                return false; // Silinecek sürücü bulunamadı
            }
            _driverRepository.Delete(driverToDelete);
            return await _driverRepository.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
            return await _driverRepository.GetAllAsync();
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            return await _driverRepository.GetByIdAsync(id);
        }

        public async Task<Driver> UpdateDriverAsync(Driver driver)
        {
            _driverRepository.Update(driver);
            await _driverRepository.SaveChangesAsync();
            return driver; // Güncellenen sürücüyü döndür
        }
    }
}
