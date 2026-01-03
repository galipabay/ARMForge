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
    public class VehicleService(IGenericRepository<Vehicle> vehicleRepository, IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IVehicleService
    {
        private readonly IGenericRepository<Vehicle> _vehicleRepository = vehicleRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        #region GetAllVehicles
        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync() // ✅ DTO
        {
            var vehicles = await _vehicleRepository.GetAllWithIncludesAsync(v => v.Shipments);

            // ✅ CurrentUserService ile filtrele
            if (!_currentUserService.IsAdmin)
            {
                vehicles = vehicles.Where(v => v.IsActive).ToList();
            }

            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }
        #endregion

        #region GetVehicle
        public async Task<VehicleDto?> GetVehicleByIdAsync(int id) // ✅ DTO + NULLABLE
        {
            var vehicle = await _vehicleRepository.GetByConditionAsync(
                v => v.Id == id && (_currentUserService.IsAdmin || v.IsActive),
                include: q => q.Include(v => v.Shipments)
            );

            return vehicle == null ? null : _mapper.Map<VehicleDto>(vehicle);
        } 
        #endregion

        #region AddVehicle
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
        #endregion

        #region UpdateVehicle
        public async Task<VehicleDto?> UpdateVehicleAsync(int id, VehicleUpdateDto vehicleDto) // ✅ DTO + ID PARAMETRE
        {
            var vehicle = await _vehicleRepository.GetByConditionAsync(v => v.Id == id);
            if (vehicle == null)
                return null;

            // ✅ PlateNumber unique kontrolü (eğer değiştiyse)
            if (vehicleDto.PlateNumber != null && vehicleDto.PlateNumber != vehicle.PlateNumber)
            {
                var exists = await _vehicleRepository.FindAsync(v => v.PlateNumber == vehicleDto.PlateNumber);
                if (exists.Any())
                    throw new InvalidOperationException("Bu plaka numarasına sahip bir araç zaten mevcut.");
            }

            // ✅ Conditional update
            _mapper.Map(vehicleDto, vehicle);
            vehicle.UpdatedAt = DateTime.UtcNow;

            _vehicleRepository.Update(vehicle);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<VehicleDto>(vehicle);
        }
        #endregion

        #region DeleteVehicle
        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByConditionAsync(v => v.Id == id && v.IsActive);
            if (vehicle == null)
                return false;

            // ✅ SOFT DELETE
            vehicle.IsActive = false;
            vehicle.UpdatedAt = DateTime.UtcNow;

            _vehicleRepository.Update(vehicle);
            await _unitOfWork.CommitAsync();
            return true;
        } 
        #endregion
    }
}
