using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
using ARMForge.Kernel.Interfaces.UnitOfWork;
using ARMForge.Types.DTOs;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        // Constructor ile IGenericRepository'yi DI (Dependency Injection) ile al
        public VehicleService(IGenericRepository<Vehicle> vehicleRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<VehicleDto> AddVehicleAsync(VehicleCreateDto vehicleDto)
        {
            var exists = await _vehicleRepository.FindAsync(v => v.PlateNumber == vehicleDto.PlateNumber);
            if (exists.Any())
                throw new InvalidOperationException("Bu plaka numarasına sahip bir araç zaten mevcut.");

            var vehicle = _mapper.Map<Vehicle>(vehicleDto);

            await _vehicleRepository.AddAsync(vehicle);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<VehicleDto>(vehicle);
        }

        //public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        //{
        //    // İş mantığı buraya gelir. Örn: Plaka numarasının benzersiz olduğunu kontrol etme.
        //    var existingVehicle = await _vehicleRepository.FindAsync(v => v.PlateNumber == vehicle.PlateNumber);
        //    if (existingVehicle.Any())
        //    {
        //        // İş kuralı ihlali, hata döndür
        //        throw new InvalidOperationException("Bu plaka numarasına sahip bir araç zaten mevcut.");
        //    }

        //    await _vehicleRepository.AddAsync(vehicle);
        //    await _vehicleRepository.SaveChangesAsync();
        //    return vehicle; // Eklenen aracı döndür
        //}

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
