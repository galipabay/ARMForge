using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? StockKeepingUnit { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? Category { get; set; }
        public int OrderItemCount { get; set; } // ✅ Ekstra: Kaç siparişte kullanıldı?
    }
}
