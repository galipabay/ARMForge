using ARMForge.Kernel.Enums;
using ARMForge.Types.DTOs;
using System.ComponentModel.DataAnnotations;
namespace ARMForge.Types.DTOs;
public class OrderCreateDto
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public DateTimeOffset RequiredDate { get; set; }

    [Required, MaxLength(32)]
    public string OrderNumber { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string DeliveryCity { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string DeliveryPostalCode { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string DeliveryCountry { get; set; } = string.Empty;

    public OrderPriority Priority { get; set; } = OrderPriority.Medium;

    [MaxLength(500)]
    public string? SpecialInstructions { get; set; }

    [Required]
    public List<OrderItemCreateDto> OrderItems { get; set; } = new();
}
