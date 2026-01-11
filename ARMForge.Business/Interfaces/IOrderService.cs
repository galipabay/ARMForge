using ARMForge.Types.DTOs;

public interface IOrderService
{
    // Queries
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int id);

    // Commands
    Task<OrderDto> AddOrderAsync(OrderCreateDto dto);
    Task<OrderDto> UpdateOrderAsync(int id, OrderUpdateDto dto);
    Task<bool> DeleteOrderAsync(int id);
    Task ConfirmOrderAsync(int id, string paymentMethod);
    Task CancelOrderAsync(int id);
    Task ShipOrderAsync(int id);
    Task MarkOrderAsPaidAsync(int id, string paymentMethod);
}
