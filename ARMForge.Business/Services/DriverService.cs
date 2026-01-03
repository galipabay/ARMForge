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
        private readonly ICurrentUserService _currentUserService;

        public DriverService(IGenericRepository<Driver> driverRepository,IMapper mapper,IUnitOfWork unitOfWork,ICurrentUserService currentUserService)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<DriverDto> AddDriverAsync(DriverCreateDto driverDto)
        {
            var driver = _mapper.Map<Driver>(driverDto);

            await _driverRepository.AddAsync(driver);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<DriverDto>(driver);
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            var driver = await _driverRepository.GetByConditionAsync(d => d.Id == id && d.IsActive);
            if (driver == null)
                return false;

            // ✅ SOFT DELETE
            driver.IsActive = false;
            driver.UpdatedAt = DateTime.UtcNow;

            _driverRepository.Update(driver); // ✅ UPDATE çağır
            await _unitOfWork.CommitAsync(); // ✅ UnitOfWork kullan
            return true;
        }
        public async Task<IEnumerable<DriverDto>> GetAllDriversAsync()
        {
            // 1️⃣ EF Core'dan User navigation property ile birlikte driverları al
            var drivers = await _driverRepository.GetAllWithIncludesAsync(d => d.User);

            if (!_currentUserService.IsAdmin)
            {
                drivers = drivers.Where(d => d.IsActive).ToList();
            }
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }

        public async Task<DriverDto?> GetDriverByIdAsync(int id)
        {
            var driver = await _driverRepository.GetByConditionAsync(
                d => d.Id == id && (_currentUserService.IsAdmin || d.IsActive),
                include: q => q.Include(d => d.User)
            );

            return driver == null ? null : _mapper.Map<DriverDto>(driver);
        }

        public async Task<DriverDto?> UpdateDriverAsync(int id, DriverUpdateDto dto)
        {
            var driver = await _driverRepository.GetByConditionAsync(d => d.Id == id, include: q => q.Include(d => d.User));
            if (driver == null)
                return null;

            _mapper.Map(dto, driver);
            driver.UpdatedAt = DateTime.UtcNow;

            _driverRepository.Update(driver);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<DriverDto>(driver);
        }
    }
}
