using ARMForge.Kernel.Enums;
using System.ComponentModel.DataAnnotations;
namespace ARMForge.Types.DTOs;
public class OrderUpdateDto
{
    public int? CustomerId { get; set; }

    public DateTimeOffset? RequiredDate { get; set; }

    [MaxLength(32)]
    public string? OrderNumber { get; set; }

    [MaxLength(32)]
    public string? PaymentMethod { get; set; }

    [MaxLength(500)]
    public string? DeliveryAddress { get; set; }

    [MaxLength(100)]
    public string? DeliveryCity { get; set; }

    [MaxLength(20)]
    public string? DeliveryPostalCode { get; set; }

    [MaxLength(50)]
    public string? DeliveryCountry { get; set; }

    public OrderPriority? Priority { get; set; }

    public OrderStatus? Status { get; set; }

    [MaxLength(500)]
    public string? SpecialInstructions { get; set; }
}
