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
    public class PurchaseOrderItemService : IPurchaseOrderItemService
    {
        private readonly IGenericRepository<PurchaseOrderItem> _purchaseOrderItemRepository;

        public PurchaseOrderItemService(IGenericRepository<PurchaseOrderItem> purchaseOrderItemRepository)
        {
            _purchaseOrderItemRepository = purchaseOrderItemRepository;
        }

        public async Task<PurchaseOrderItem> AddPurchaseOrderItemAsync(PurchaseOrderItem purchaseOrderItem)
        {
            await _purchaseOrderItemRepository.AddAsync(purchaseOrderItem);
            await _purchaseOrderItemRepository.SaveChangesAsync();
            return purchaseOrderItem;
        }

        public async Task<bool> DeletePurchaseOrderItemAsync(int id)
        {
            var purchaseOrderItemToDelete = await _purchaseOrderItemRepository.GetByIdAsync(id);
            if (purchaseOrderItemToDelete == null) return false;

            _purchaseOrderItemRepository.Delete(purchaseOrderItemToDelete);
            return await _purchaseOrderItemRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<PurchaseOrderItem>> GetAllPurchaseOrderItemsAsync()
        {
            return await _purchaseOrderItemRepository.GetAllAsync();
        }

        public async Task<PurchaseOrderItem> GetPurchaseOrderItemByIdAsync(int id)
        {
            return await _purchaseOrderItemRepository.GetByIdAsync(id);
        }

        public async Task<PurchaseOrderItem> UpdatePurchaseOrderItemAsync(PurchaseOrderItem purchaseOrderItem)
        {
            _purchaseOrderItemRepository.Update(purchaseOrderItem);
            await _purchaseOrderItemRepository.SaveChangesAsync();
            return purchaseOrderItem;
        }
    }
}
