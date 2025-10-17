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
    public class CustomerService(IGenericRepository<Customer> customerRepository, IMapper mapper,IUnitOfWork unitOfWork,ICurrentUserService currentUserService) : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository = customerRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(bool includeInactive = false)
        {
            var isAdmin = _currentUserService.IsAdmin;
            var query = _customerRepository.GetQueryable(includeInactive: isAdmin);

            var customers = await query.Include(c => c.Orders).ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByConditionAsync(
                c => c.Id == id && (_currentUserService.IsAdmin || c.IsActive), // ✅ Security
                include: q => q.Include(c => c.Orders)
            );

            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> AddCustomerAsync(CustomerCreateDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CustomerDto>(customer);
        }
        public async Task<CustomerDto> UpdateCustomerAsync(int id, CustomerUpdateDto customerUpdateDto)
        {
            var existingCustomer = await _customerRepository.GetByConditionAsync(c => c.Id == id) ?? throw new InvalidOperationException($"Customer with id {id} not found");
            _mapper.Map(customerUpdateDto, existingCustomer);
            existingCustomer.UpdatedAt = DateTime.UtcNow;

            _customerRepository.Update(existingCustomer);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CustomerDto>(existingCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByConditionAsync(c => c.Id == id && c.IsActive);
            if (customer == null)
                return false;

            customer.IsActive = false;
            customer.UpdatedAt = DateTime.UtcNow;

            _customerRepository.Update(customer);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
