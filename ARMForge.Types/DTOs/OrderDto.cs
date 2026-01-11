using ARMForge.Kernel.Enums;
using ARMForge.Types.DTOs;
namespace ARMForge.Types.DTOs;
public class OrderDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public string CustomerCompanyName { get; set; } = string.Empty;

    public DateTimeOffset OrderDate { get; set; }
    public DateTimeOffset RequiredDate { get; set; }
    public DateTimeOffset? ShippedDate { get; set; }

    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal TotalWeight { get; set; }
    public decimal TotalVolume { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public string? PaymentMethod { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;
    public string DeliveryCity { get; set; } = string.Empty;
    public string DeliveryPostalCode { get; set; } = string.Empty;
    public string DeliveryCountry { get; set; } = string.Empty;

    public OrderPriority Priority { get; set; }
    public string? SpecialInstructions { get; set; }

    public List<ShipmentInfoDto> Shipments { get; set; } = new();
}
