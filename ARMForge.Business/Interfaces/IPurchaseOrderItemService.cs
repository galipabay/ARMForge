using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IPurchaseOrderItemService
    {
        Task<IEnumerable<PurchaseOrderItem>> GetAllPurchaseOrderItemsAsync();
        Task<PurchaseOrderItem> GetPurchaseOrderItemByIdAsync(int id);
        Task<PurchaseOrderItem> AddPurchaseOrderItemAsync(PurchaseOrderItem purchaseOrderItem);
        Task<PurchaseOrderItem> UpdatePurchaseOrderItemAsync(PurchaseOrderItem purchaseOrderItem);
        Task<bool> DeletePurchaseOrderItemAsync(int id);
    }
}
