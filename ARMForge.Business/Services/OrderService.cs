using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Customer> _customerRepository;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<Customer> customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            // Müşteri nesnesinin mevcut olduğunu varsayıyoruz, bu yüzden ID'si 0'dan farklı.
            // O yüzden EF'ye bu nesneyi eklememesini, sadece bağlamasını söylüyoruz.
            if (order.Customer != null && order.Customer.Id != 0)
            {
                //_context.Customers.Attach(order.Customer);
                await _customerRepository.AttachAsync(order.Customer);
            }
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(id);
            if (orderToDelete == null) return false;

            _orderRepository.Delete(orderToDelete);
            return await _orderRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
            return order;
        }
    }
}
