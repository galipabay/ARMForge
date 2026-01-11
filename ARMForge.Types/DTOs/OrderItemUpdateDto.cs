using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class OrderItemUpdateDto
    {
        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal? UnitPrice { get; set; }

        public string? BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? StorageLocation { get; set; }
        public string? SupplierCode { get; set; }
        public string? SerialNumber { get; set; }
    }
}
