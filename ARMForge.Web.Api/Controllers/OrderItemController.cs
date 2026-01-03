using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARMForge.Web.Api.Controllers
{
    [Route("api/orders/{orderId:int}/items")]
    [ApiController]
    [Authorize]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        // 📌 GET: api/orders/{orderId}/items
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems(int orderId)
        {
            var items = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(items);
        }

        // 📌 GET: api/orders/{orderId}/items/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderItemById(int orderId, int id)
        {
            var item = await _orderItemService.GetOrderItemByIdAsync(id);

            if (item == null || item.OrderId != orderId)
                return NotFound();

            return Ok(item);
        }

        // 📌 POST: api/orders/{orderId}/items
        [HttpPost]
        public async Task<IActionResult> AddOrderItem(
            int orderId,
            [FromBody] OrderItemCreateDto dto)
        {
            var createdItem = await _orderItemService.AddOrderItemAsync(orderId, dto);

            return CreatedAtAction(
                nameof(GetOrderItemById),
                new { orderId, id = createdItem.Id },
                createdItem);
        }

        // 📌 PUT: api/orders/{orderId}/items/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrderItem(
            int orderId,
            int id,
            [FromBody] OrderItemUpdateDto dto)
        {
            var updatedItem = await _orderItemService.UpdateOrderItemAsync(id, dto);

            if (updatedItem == null || updatedItem.OrderId != orderId)
                return NotFound();

            return Ok(updatedItem);
        }

        // 📌 DELETE: api/orders/{orderId}/items/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrderItem(int orderId, int id)
        {
            var deleted = await _orderItemService.DeleteOrderItemAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
