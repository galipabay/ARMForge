using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        // Order
        public int OrderId { get; set; }

        // Product
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? StockKeepingUnit { get; set; }
        public string? Category { get; set; }

        // Financial
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }

        // Logistics
        public decimal TotalWeight { get; set; }
        public decimal TotalVolume { get; set; }

        public string? BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? StorageLocation { get; set; }

        // Audit
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }

}
