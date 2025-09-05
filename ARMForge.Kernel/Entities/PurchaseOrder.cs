using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class PurchaseOrder : BaseEntity
    {
        [Required]
        public int SupplierId { get; set; }

        [Required]
        public DateTimeOffset OrderDate { get; set; }

        public DateTimeOffset? ExpectedDeliveryDate { get; set; }

        [MaxLength(32)]
        public string Status { get; set; } = "Created";

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        // Gezinti Özellikleri (Navigation Properties)
        public Supplier Supplier { get; set; } = null!;
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    }
}
