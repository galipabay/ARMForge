using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
using ARMForge.Kernel.Interfaces.UnitOfWork;
using ARMForge.Types.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DriverService(IGenericRepository<Driver> driverRepository,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DriverDto> CreateDriverAsync(DriverCreateDto driverDto)
        {
            var driver = _mapper.Map<Driver>(driverDto);

            await _driverRepository.AddAsync(driver);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<DriverDto>(driver);
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
        public async Task<IEnumerable<DriverDto>> GetAllDriversAsync()
        {
            // 1️⃣ EF Core'dan User navigation property ile birlikte driverları al
            var drivers = await _driverRepository.GetAllWithIncludesAsync(d => d.User);

            // 2️⃣ AutoMapper ile DTO'ya çevir
            var driverDtos = _mapper.Map<IEnumerable<DriverDto>>(drivers);

            // 3️⃣ DTO listesi dön
            return driverDtos;
        }

        public async Task<DriverDto?> GetDriverByIdAsync(int id)
        {
            // 1️⃣ Driver'ı User navigation property ile al
            var driver = await _driverRepository.GetByConditionAsync(
                d => d.Id == id,
                include: q => q.Include(d => d.User)
            );

            if (driver == null)
                return null;

            // 2️⃣ Entity → DTO mapping (AutoMapper veya manuel)
            var driverDto = _mapper.Map<DriverDto>(driver);

            return driverDto;
        }

        public async Task<DriverDto?> UpdateDriverAsync(int id, DriverUpdateDto dto)
        {
            var driver = await _driverRepository.GetByConditionAsync(d => d.Id == id, include: q => q.Include(d => d.User));
            if (driver == null)
                return null;

            // Alanları güncelle
            if (dto.IsOnDuty.HasValue)
                driver.IsOnDuty = dto.IsOnDuty.Value;

            if (dto.LicenseType.HasValue)
                driver.LicenseType = dto.LicenseType.Value; // artık enum ile uyumlu

            if (dto.IsAvailable.HasValue)
                driver.IsAvailable = dto.IsAvailable.Value;

            // UpdatedAt güncelle
            driver.UpdatedAt = DateTime.UtcNow;

            //_driverRepository.Update(driver);
            await _driverRepository.SaveChangesAsync();
            // DTO’ya map et
            return _mapper.Map<DriverDto>(driver);
        }
    }
}
