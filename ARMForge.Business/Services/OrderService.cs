using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Enums;
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
                o => _currentUserService.IsAdmin || o.IsActive,
                o => o.Customer,
                o => o.Shipments
            );

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
            #region Customer validation
            var customerExists = await _customerRepository
                .GetByConditionAsync(c => c.Id == orderDto.CustomerId && c.IsActive) ?? throw new InvalidOperationException("Müşteri bulunamadı veya aktif değil.");
            #endregion

            #region OrderNumber unique check
            if (!string.IsNullOrEmpty(orderDto.OrderNumber))
            {
                var orderNumberExists = await _orderRepository
                    .FindAsync(o => o.OrderNumber == orderDto.OrderNumber);

                if (orderNumberExists.Any())
                    throw new InvalidOperationException("Bu sipariş numarası zaten mevcut.");
            }
            #endregion

            #region Required
            if (string.IsNullOrEmpty(orderDto.DeliveryAddress))
                throw new InvalidOperationException("Teslimat adresi zorunludur.");

            if (string.IsNullOrEmpty(orderDto.DeliveryCity))
                throw new InvalidOperationException("Teslimat şehri zorunludur.");
            #endregion

            // 🔹 MAP
            var order = _mapper.Map<Order>(orderDto);

            // 🔹 DOMAIN KURALLARI (ÖNEMLİ)
            order.SetRequiredDate(orderDto.RequiredDate);
            order.ChangePriority(orderDto.Priority);

            if (orderDto.OrderItems != null && orderDto.OrderItems.Count != 0)
            {
                var productIds = orderDto.OrderItems.Select(x => x.ProductId).ToList();

                var products = await _productRepository
                    .FindAsync(p => productIds.Contains(p.Id) && p.IsActive);

                if (products.Count() != productIds.Count)
                    throw new InvalidOperationException("Bazı ürünler bulunamadı veya aktif değil.");

                decimal totalAmount = 0;
                decimal totalWeight = 0;
                decimal totalVolume = 0;

                foreach (var item in orderDto.OrderItems)
                {
                    var product = products.First(p => p.Id == item.ProductId);

                    if (product.StockQuantity < item.Quantity)
                        throw new InvalidOperationException(
                            $"{product.Name} için yeterli stok yok. Mevcut: {product.StockQuantity}");

                    totalAmount += product.UnitPrice * item.Quantity;
                    totalWeight += product.UnitWeight * item.Quantity;
                    totalVolume += product.UnitVolume * item.Quantity;
                }

                // 🔹 TOTALS SADECE ENTITY ÜZERİNDEN
                order.SetTotals(totalAmount, totalWeight, totalVolume);
            }

            await _orderRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> UpdateOrderAsync(int id, OrderUpdateDto orderDto)
        {
            var order = await _orderRepository.GetByConditionAsync(o => o.Id == id) ?? throw new KeyNotFoundException("Order bulunamadı.");

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
                var customerExists = await _customerRepository.GetByConditionAsync(c => c.Id == orderDto.CustomerId.Value && c.IsActive) ?? throw new InvalidOperationException("Müşteri bulunamadı veya aktif değil.");
            }

            if (!string.IsNullOrEmpty(orderDto.DeliveryAddress) && orderDto.DeliveryAddress.Length > 500)
                throw new InvalidOperationException("Teslimat adresi çok uzun.");

            _mapper.Map(orderDto, order);
            order.UpdatedAt = DateTime.UtcNow;

            if (orderDto.Status.HasValue)
            {
                switch (orderDto.Status.Value)
                {
                    case OrderStatus.Confirmed:
                        order.MarkAsPaid(orderDto.PaymentMethod ?? "Unknown");
                        break;

                    case OrderStatus.Shipped:
                        order.Ship();
                        break;

                    case OrderStatus.Cancelled:
                        order.Cancel();
                        break;

                    default:
                        throw new InvalidOperationException("Geçersiz status geçişi.");
                }
            }

            if (orderDto.Priority.HasValue)
            {
                order.ChangePriority(orderDto.Priority.Value);
            }


            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository
                .GetByConditionAsync(o => o.Id == id && o.IsActive);

            if (order == null)
                return false;

            order.Deactivate();
            order.UpdatedAt = DateTime.UtcNow;

            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task ConfirmOrderAsync(int id, string paymentMethod)
        {
            var order = await _orderRepository
                .GetByConditionAsync(o => o.Id == id && o.IsActive)
                ?? throw new InvalidOperationException("Sipariş bulunamadı.");

            // ✔ ödeme + draft → confirmed
            order.MarkAsPaid(paymentMethod);

            order.UpdatedAt = DateTime.UtcNow;

            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task CancelOrderAsync(int id)
        {
            var order = await _orderRepository
                .GetByConditionAsync(o => o.Id == id && o.IsActive)
                ?? throw new InvalidOperationException("Sipariş bulunamadı.");

            // ✔ entity içi kural
            order.Cancel();

            order.UpdatedAt = DateTime.UtcNow;

            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task ShipOrderAsync(int id)
        {
            var order = await _orderRepository
                .GetByConditionAsync(o => o.Id == id && o.IsActive)
                ?? throw new InvalidOperationException("Sipariş bulunamadı.");

            // ✔ ödeme + confirmed kontrolü entity’de
            order.Ship();

            order.UpdatedAt = DateTime.UtcNow;

            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task MarkOrderAsPaidAsync(int id, string paymentMethod)
        {
            var order = await _orderRepository
                .GetByConditionAsync(o => o.Id == id && o.IsActive)
                ?? throw new InvalidOperationException("Sipariş bulunamadı.");

            // ✔ status / paymentStatus entity yönetiyor
            order.MarkAsPaid(paymentMethod);

            order.UpdatedAt = DateTime.UtcNow;

            _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();
        }
    }
}
