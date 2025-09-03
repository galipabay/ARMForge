using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IGenericRepository<Vehicle> _vehicleRepository;

        // Constructor ile IGenericRepository'yi DI (Dependency Injection) ile al
        public VehicleService(IGenericRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            // İş mantığı buraya gelir. Örn: Plaka numarasının benzersiz olduğunu kontrol etme.
            var existingVehicle = await _vehicleRepository.FindAsync(v => v.PlateNumber == vehicle.PlateNumber);
            if (existingVehicle.Any())
            {
                // İş kuralı ihlali, hata döndür
                throw new InvalidOperationException("Bu plaka numarasına sahip bir araç zaten mevcut.");
            }

            await _vehicleRepository.AddAsync(vehicle);
            await _vehicleRepository.SaveChangesAsync();
            return vehicle; // Eklenen aracı döndür
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicleToDelete = await _vehicleRepository.GetByIdAsync(id);
            if (vehicleToDelete == null)
            {
                return false; // Silinecek araç bulunamadı
            }

            _vehicleRepository.Delete(vehicleToDelete);
            return await _vehicleRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _vehicleRepository.GetAllAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _vehicleRepository.GetByIdAsync(id);
        }

        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            // İş mantığı buraya gelir. Örn: Güncelleme kuralları.
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveChangesAsync();
            return vehicle;
        }
    }
}
