using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<Invoice?> GetInvoiceByIdAsync(int id);
        Task<Invoice> AddInvoiceAsync(Invoice invoice);
        Task<Invoice> UpdateInvoiceAsync(Invoice invoice);
        Task<bool> DeleteInvoiceAsync(int id);
    }
}
