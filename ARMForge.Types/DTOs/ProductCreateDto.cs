using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ProductCreateDto
    {
        [Required, MaxLength(128)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(64)]
        public string? StockKeepingUnit { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [MaxLength(64)]
        public string? Category { get; set; }
    }
}
