using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARMForge.Types.DTOs;

namespace ARMForge.Business.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(bool includeInactive = false);
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<CustomerDto> AddCustomerAsync(CustomerCreateDto customerDto);
        Task<CustomerDto> UpdateCustomerAsync(int id,CustomerUpdateDto customerUpdateDto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
