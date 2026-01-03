using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
   public class ProductUpdateDto
    {
        [MaxLength(128)]
        public string? Name { get; set; }

        [MaxLength(64)]
        public string? StockKeepingUnit { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int? StockQuantity { get; set; }

        [MaxLength(64)]
        public string? Category { get; set; }
    }
}
