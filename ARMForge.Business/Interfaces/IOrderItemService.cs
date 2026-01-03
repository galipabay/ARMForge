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
        Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync(); // opsiyonel
        Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId);

        Task<OrderItemDto?> GetOrderItemByIdAsync(int id);

        Task<OrderItemDto> AddOrderItemAsync(int orderId, OrderItemCreateDto dto);

        Task<OrderItemDto?> UpdateOrderItemAsync(int id, OrderItemUpdateDto dto);

        Task<bool> DeleteOrderItemAsync(int id);
    }
}
