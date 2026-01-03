using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
using ARMForge.Kernel.Interfaces.UnitOfWork;
using ARMForge.Types.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class OrderService(
    IGenericRepository<Order> orderRepository,
    IGenericRepository<Customer> customerRepository,
    IGenericRepository<Product> productRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository = orderRepository;
        private readonly IGenericRepository<Customer> _customerRepository = customerRepository;
        private readonly IGenericRepository<Product> _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllWithIncludesAsync(
                o => o.Customer,
                o => o.Shipments,
                o => o.OrderItems
            );

            // ✅ CurrentUserService ile filtrele
            if (!_currentUserService.IsAdmin)
            {
                orders = orders.Where(o => o.IsActive).ToList();
            }

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByConditionAsync(
                    o => o.Id == id && (_currentUserService.IsAdmin || o.IsActive),
                    include: q => q.Include(o => o.Customer)
                                   .Include(o => o.Shipments)
                                   .Include(o => o.OrderItems)
                                   .ThenInclude(oi => oi.Product)
                );

            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> AddOrderAsync(OrderCreateDto orderDto)
        {
            // ✅ Customer exists check
            var customerExists = await _customerRepository.GetByConditionAsync(c => c.Id == orderDto.CustomerId && c.IsActive);
            if (customerExists == null)
                throw new InvalidOperationException("Müşteri bulunamadı veya aktif değil.");

            // ✅ OrderNumber unique check
            var orderNumberExists = await _orderRepository.FindAsync(o => o.OrderNumber == orderDto.OrderNumber);
            if (orderNumberExists.Any())
                throw new InvalidOperationException("Bu sipariş numarası zaten mevcut.");

            // ✅ Delivery Validation (SADECE BUNLAR)
            if (string.IsNullOrEmpty(orderDto.DeliveryAddress))
                throw new InvalidOperationException("Teslimat adresi zorunludur.");

            if (string.IsNullOrEmpty(orderDto.DeliveryCity))
                throw new InvalidOperationException("Teslimat şehri zorunludur.");

            var order = _mapper.Map<Order>(orderDto);

            // ✅ ORDER ITEMS İŞLEME
            if (orderDto.OrderItems != null && orderDto.OrderItems.Any())
            {
                foreach (var itemDto in orderDto.OrderItems)
                {
                    // Product exists check
                    var productExists = await _productRepository.GetByConditionAsync(p => p.Id == itemDto.ProductId && p.IsActive);
                    if (productExists == null)
                        throw new InvalidOperationException($"Product ID {itemDto.ProductId} bulunamadı veya aktif değil.");

                    // Stock check (eğer satış yapıyorsan)
                    if (productExists.StockQuantity < itemDto.Quantity)
                        throw new InvalidOperationException($"{productExists.Name} ürününden yeterli stok yok. Mevcut: {productExists.StockQuantity}, İstenen: {itemDto.Quantity}");
                }

                decimal totalAmount = 0;
                decimal totalWeight = 0;
                decimal totalVolume = 0;

                foreach (var itemDto in orderDto.OrderItems)
                {
                    var product = await _productRepository.GetByConditionAsync(
                        p => p.Id == itemDto.ProductId && p.IsActive
                    );

                    if (product == null)
                        throw new InvalidOperationException($"Product ID {itemDto.ProductId} bulunamadı.");

                    if (product.StockQuantity < itemDto.Quantity)
                        throw new InvalidOperationException($"{product.Name} için yeterli stok yok.");

                    totalAmount += itemDto.Quantity * product.UnitPrice;
                    totalWeight += itemDto.Weight;
                    totalVolume += itemDto.Volume;
                }

                order.TotalAmount = totalAmount;
                order.TotalWeight = totalWeight;
                order.TotalVolume = totalVolume;
            }

            await _orderRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> UpdateOrderAsync(int id, OrderUpdateDto orderDto)
        {
            var order = await _orderRepository.GetByConditionAsync(o => o.Id == id);
            if (order == null)
                return null;

            // ✅ OrderNumber unique check (eğer değiştiyse)
            if (!string.IsNullOrEmpty(orderDto.OrderNumber) && orderDto.OrderNumber != order.OrderNumber)
            {
                var orderNumberExists = await _orderRepository.FindAsync(o => o.OrderNumber == orderDto.OrderNumber);
                if (orderNumberExists.Any())
                    throw new InvalidOperationException("Bu sipariş numarası zaten mevcut.");
            }

            // ✅ Customer exists check (eğer değiştiyse)
            if (orderDto.CustomerId.HasValue)
            {
                var customerExists = await _customerRepository.GetByConditionAsync(c => c.Id == orderDto.CustomerId.Value && c.IsActive);
                if (customerExists == null)
                    throw new InvalidOperationException("Müşteri bulunamadı veya aktif değil.");
            }

            if (!string.IsNullOrEmpty(orderDto.DeliveryAddress) && orderDto.DeliveryAddress.Length > 500)
                throw new InvalidOperationException("Teslimat adresi çok uzun.");

            _mapper.Map(orderDto, order);
            order.UpdatedAt = DateTime.UtcNow;

            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByConditionAsync(o => o.Id == id && o.IsActive);
            if (order == null)
                return false;

            order.IsActive = false;
            order.UpdatedAt = DateTime.UtcNow;

            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
