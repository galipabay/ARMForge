using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Subtotal { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Weight { get; set; } // Toplam ağırlık (kg)

        [Range(0, double.MaxValue)]
        public decimal Volume { get; set; } // Toplam hacim (m³)

        public string? BatchNumber { get; set; } // Parti numarası
        public DateTime? ExpiryDate { get; set; } // Son kullanma tarihi (gıda/ilaç)

        [MaxLength(64)]
        public string? StorageLocation { get; set; } // Depodaki yeri "A-12-3"
    }
}
