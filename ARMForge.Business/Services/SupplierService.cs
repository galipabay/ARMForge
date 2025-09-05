using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IGenericRepository<Supplier> _supplierRepository;

        public SupplierService(IGenericRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            await _supplierRepository.AddAsync(supplier);
            await _supplierRepository.SaveChangesAsync();
            return supplier;
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplierToDelete = await _supplierRepository.GetByIdAsync(id);
            if (supplierToDelete == null) return false;

            _supplierRepository.Delete(supplierToDelete);
            return await _supplierRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _supplierRepository.GetByIdAsync(id);
        }

        public async Task<Supplier> UpdateSupplierAsync(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
            await _supplierRepository.SaveChangesAsync();
            return supplier;
        }
    }
}
