using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IOrderItemService
    {
        /// <summary>
        /// Tüm order itemları getirir.
        /// </summary>
        /// <param name="includeInactive">false ise sadece aktif itemlar getirilir.</param>
        Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync(bool includeInactive = false);

        /// <summary>
        /// Belirli bir siparişe ait order itemları getirir.
        /// </summary>
        /// <param name="includeInactive">false ise sadece aktif itemlar getirilir.</param>
        Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId, bool includeInactive = false);

        /// <summary>
        /// Id’ye göre tek bir order item getirir.
        /// </summary>
        Task<OrderItemDto?> GetOrderItemByIdAsync(int id);

        /// <summary>
        /// Yeni bir order item ekler.
        /// </summary>
        /// <param name="orderId">Hangi siparişe ekleneceği.</param>
        Task<OrderItemDto> AddOrderItemAsync(int orderId, OrderItemCreateDto dto);

        /// <summary>
        /// Var olan order itemı günceller (partial update destekli).
        /// </summary>
        Task<OrderItemDto?> UpdateOrderItemAsync(int id, OrderItemUpdateDto dto);

        /// <summary>
        /// Order item siler (soft delete veya domain kurallarına göre işlem yapar).
        /// </summary>
        Task<bool> DeleteOrderItemAsync(int id);

        /// <summary>
        /// Opsiyonel: iade veya miktar ayarlaması için kullanılabilir.
        /// </summary>
        Task<OrderItemDto?> AdjustQuantityAsync(int id, int newQuantity);
    }
}
