using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
using ARMForge.Kernel.Interfaces.UnitOfWork;
using ARMForge.Types.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ARMForge.Business.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemService(
            IGenericRepository<OrderItem> orderItemRepository,
            IGenericRepository<Order> orderRepository,
            IGenericRepository<Product> productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // 📌 GET ALL (opsiyonel)
        public async Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync()
        {
            var items = await _orderItemRepository.GetAllWithIncludesAsync(
                oi => oi.Product,
                oi => oi.Order
            );

            return _mapper.Map<IEnumerable<OrderItemDto>>(items);
        }

        // 📌 GET BY ORDER
        public async Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var items = await _orderItemRepository.FindAsync(oi => oi.OrderId == orderId);
            return _mapper.Map<IEnumerable<OrderItemDto>>(items);
        }

        // 📌 GET BY ID
        public async Task<OrderItemDto?> GetOrderItemByIdAsync(int id)
        {
            var item = await _orderItemRepository.GetByConditionAsync(
                oi => oi.Id == id,
                include: q => q
                    .Include(x => x.Product)
                    .Include(x => x.Order)
            );

            return item == null ? null : _mapper.Map<OrderItemDto>(item);
        }

        public async Task<OrderItemDto> AddOrderItemAsync(int orderId, OrderItemCreateDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product == null)
                throw new InvalidOperationException("Ürün bulunamadı.");

            if (product.StockQuantity < dto.Quantity)
                throw new InvalidOperationException("Yetersiz stok.");

            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = dto.ProductId,

                Quantity = dto.Quantity,
                UnitPrice = product.UnitPrice,
                Subtotal = dto.Quantity * product.UnitPrice,

                // ✅ Lojistik snapshot
                Weight = product.UnitWeight * dto.Quantity,
                Volume = product.UnitVolume * dto.Quantity,

                BatchNumber = dto.BatchNumber,
                ExpiryDate = dto.ExpiryDate,
                StorageLocation = dto.StorageLocation
            };

            // (Opsiyonel ama önerilir)
            product.StockQuantity -= dto.Quantity;

            await _orderItemRepository.AddAsync(orderItem);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<OrderItemDto>(orderItem);
        }

        // 📌 UPDATE
        public async Task<OrderItemDto?> UpdateOrderItemAsync(int id, OrderItemUpdateDto dto)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
                return null;

            // Quantity değiştiyse subtotal yeniden hesaplanır
            if (dto.Quantity.HasValue)
            {
                if (dto.Quantity.Value <= 0)
                    throw new InvalidOperationException("Miktar 0'dan büyük olmalıdır.");

                orderItem.Quantity = dto.Quantity.Value;
                orderItem.Subtotal = orderItem.Quantity * orderItem.UnitPrice;
            }

            // Diğer lojistik alanlar
            if (dto.BatchNumber != null)
                orderItem.BatchNumber = dto.BatchNumber;

            if (dto.ExpiryDate.HasValue)
                orderItem.ExpiryDate = dto.ExpiryDate;

            if (dto.StorageLocation != null)
                orderItem.StorageLocation = dto.StorageLocation;

            orderItem.UpdatedAt = DateTime.UtcNow;

            _orderItemRepository.Update(orderItem);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<OrderItemDto>(orderItem);
        }

        // 📌 DELETE
        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var item = await _orderItemRepository.GetByIdAsync(id);
            if (item == null) return false;

            _orderItemRepository.Delete(item);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
