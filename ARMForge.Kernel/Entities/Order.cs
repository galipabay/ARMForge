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

        [MaxLength(64)]
        public string Status { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        [MaxLength(32)]
        public string OrderNumber { get; set; } = string.Empty;

        public ICollection<Shipment> Shipments { get; set; } = [];
	}
}
