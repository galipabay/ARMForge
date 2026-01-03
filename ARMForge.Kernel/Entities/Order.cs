using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset RequiredDate { get; set; }
        public DateTimeOffset? ShippedDate { get; set; }

        [Required, MaxLength(64)]
        public string Status { get; set; } = "Pending";

        [Range(0.01, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required, MaxLength(32)]
        public string OrderNumber { get; set; } = string.Empty;

        [MaxLength(32)]
        public string? PaymentMethod { get; set; } // "Kredi Kartı", "Havale", "Nakit"

        [MaxLength(32)]
        public string? PaymentStatus { get; set; } // "Ödenmedi", "Ödendi", "İade"

        public DateTimeOffset? PaymentDate { get; set; }

        [Required, MaxLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string DeliveryCity { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string DeliveryPostalCode { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string DeliveryCountry { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Priority { get; set; } = "Normal";

        [MaxLength(500)]
        public string? SpecialInstructions { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalWeight { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalVolume { get; set; }

        [MaxLength(50)]
        public string? InternalReference { get; set; }

        public int? SalesPersonId { get; set; }
        public User? SalesPerson { get; set; }

        public ICollection<Shipment> Shipments { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
