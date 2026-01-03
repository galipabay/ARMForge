using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class PurchaseOrderItem : BaseEntity
    {
        // Yabancı Anahtarlar
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Subtotal { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Weight { get; set; } // Birim ağırlık

        [Range(0, double.MaxValue)]
        public decimal? Volume { get; set; } // Birim hacim

        [MaxLength(64)]
        public string? BatchNumber { get; set; } // Parti numarası

        public DateTime? ExpiryDate { get; set; } // SKT

        // Gezinti Özellikleri (Navigation Properties)
        public PurchaseOrder PurchaseOrder { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
