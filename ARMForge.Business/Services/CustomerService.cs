using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
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
    public class CustomerService(IGenericRepository<Customer> customerRepository, IMapper mapper) : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository = customerRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customerToDelete = await _customerRepository.GetByIdAsync(id);
            if (customerToDelete == null)
            {
                return false;
            }
            _customerRepository.Delete(customerToDelete);
            return await _customerRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            // Repository'den entity'leri çek (Orders'ı dahil etmeyi unutma)
            var customers = await _customerRepository.GetAllWithIncludesAsync(c => c.Orders);

            // AutoMapper ile List<Customer>'ı List<CustomerDto>'ya dönüştür
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return customerDtos;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();
            return customer;
        }
    }
}
