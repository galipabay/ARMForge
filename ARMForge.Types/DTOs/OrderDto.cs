using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerCompanyName { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset RequiredDate { get; set; }
        public DateTimeOffset? ShippedDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string OrderNumber { get; set; } = string.Empty;

        // Shipment bilgileri
        public List<ShipmentInfoDto> Shipments { get; set; } = new();
    }
}
