using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IGenericRepository<PurchaseOrder> _purchaseOrderRepository;

        public PurchaseOrderService(IGenericRepository<PurchaseOrder> purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<PurchaseOrder> AddPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            await _purchaseOrderRepository.AddAsync(purchaseOrder);
            await _purchaseOrderRepository.SaveChangesAsync();
            return purchaseOrder;
        }

        public async Task<bool> DeletePurchaseOrderAsync(int id)
        {
            var purchaseOrderToDelete = await _purchaseOrderRepository.GetByIdAsync(id);
            if (purchaseOrderToDelete == null) return false;

            _purchaseOrderRepository.Delete(purchaseOrderToDelete);
            return await _purchaseOrderRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync()
        {
            return await _purchaseOrderRepository.GetAllAsync();
        }

        public async Task<PurchaseOrder> GetPurchaseOrderByIdAsync(int id)
        {
            return await _purchaseOrderRepository.GetByIdAsync(id);
        }

        public async Task<PurchaseOrder> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderRepository.Update(purchaseOrder);
            await _purchaseOrderRepository.SaveChangesAsync();
            return purchaseOrder;
        }
    }
}
