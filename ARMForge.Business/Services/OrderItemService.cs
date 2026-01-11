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


        // 📌 GET ALL
        public async Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync(bool includeInactive = false)
        {
            var items = await _orderItemRepository.GetAllWithIncludesAsync(
                oi => oi.Product,
                oi => oi.Order
            );

            return _mapper.Map<IEnumerable<OrderItemDto>>(items);
        }

        // 📌 GET BY ORDER
        public async Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId, bool includeInactive = false)
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

        // 📌 ADD
        public async Task<OrderItemDto> AddOrderItemAsync(int orderId, OrderItemCreateDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new InvalidOperationException("İlgili sipariş bulunamadı.");

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
                Weight = product.UnitWeight * dto.Quantity,
                Volume = product.UnitVolume * dto.Quantity,
                BatchNumber = dto.BatchNumber,
                ExpiryDate = dto.ExpiryDate,
                StorageLocation = dto.StorageLocation
            };

            // stok güncelle
            product.StockQuantity -= dto.Quantity;

            // order totals güncelle
            order.IncreaseTotals(orderItem.Subtotal, orderItem.Weight, orderItem.Volume);

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

            var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
            if (product == null)
                throw new InvalidOperationException("Ürün bulunamadı.");

            var order = await _orderRepository.GetByIdAsync(orderItem.OrderId);
            if (order == null)
                throw new InvalidOperationException("Sipariş bulunamadı.");

            // eski totals'ı çıkar
            order.DecreaseTotals(orderItem.Subtotal, orderItem.Weight, orderItem.Volume);

            // quantity değiştiyse stok güncelle
            if (dto.Quantity.HasValue)
            {
                var quantityDiff = dto.Quantity.Value - orderItem.Quantity;
                if (quantityDiff > 0 && product.StockQuantity < quantityDiff)
                    throw new InvalidOperationException("Yetersiz stok.");

                product.StockQuantity -= quantityDiff; // negatifse artı olacak, pozitifse eksi olacak, mantık doğru


                product.StockQuantity -= quantityDiff;
                orderItem.Quantity = dto.Quantity.Value;
                orderItem.Subtotal = orderItem.Quantity * orderItem.UnitPrice;
                orderItem.Weight = product.UnitWeight * orderItem.Quantity;
                orderItem.Volume = product.UnitVolume * orderItem.Quantity;
            }

            if (dto.BatchNumber != null)
                orderItem.BatchNumber = dto.BatchNumber;
            if (dto.ExpiryDate.HasValue)
                orderItem.ExpiryDate = dto.ExpiryDate;
            if (dto.StorageLocation != null)
                orderItem.StorageLocation = dto.StorageLocation;

            order.IncreaseTotals(orderItem.Subtotal, orderItem.Weight, orderItem.Volume);

            orderItem.UpdatedAt = DateTime.UtcNow;
            _orderItemRepository.Update(orderItem);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<OrderItemDto>(orderItem);
        }

        // 📌 DELETE
        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
                return false;

            var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
            if (product == null)
                throw new InvalidOperationException("Ürün bulunamadı, stok iadesi yapılamadı.");

            var order = await _orderRepository.GetByIdAsync(orderItem.OrderId);
            if (order == null)
                throw new InvalidOperationException("Sipariş bulunamadı, totals güncellenemedi.");

            // stok iadesi
            product.StockQuantity += orderItem.Quantity;

            // order totals güncelle
            order.DecreaseTotals(orderItem.Subtotal, orderItem.Weight, orderItem.Volume);

            _orderItemRepository.Delete(orderItem);
            await _unitOfWork.CommitAsync();

            return true;
        }

        // 📌 ADJUST QUANTITY
        public Task<OrderItemDto?> AdjustQuantityAsync(int id, int newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
