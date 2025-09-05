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
    public class OrderItemService : IOrderItemService
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;

        public OrderItemService(IGenericRepository<OrderItem> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _orderItemRepository.GetAllAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _orderItemRepository.GetByIdAsync(id);
        }

        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            await _orderItemRepository.AddAsync(orderItem);
            await _orderItemRepository.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
        {
            _orderItemRepository.Update(orderItem);
            await _orderItemRepository.SaveChangesAsync();
            return orderItem;
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItemToDelete = await _orderItemRepository.GetByIdAsync(id);
            if (orderItemToDelete == null) return false;

            _orderItemRepository.Delete(orderItemToDelete);
            return await _orderItemRepository.SaveChangesAsync() > 0;
        }
    }
}
