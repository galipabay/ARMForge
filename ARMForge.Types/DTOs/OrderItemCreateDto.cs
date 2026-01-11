using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class OrderItemCreateDto
    {
        [Required(ErrorMessage = "Ürün ID zorunludur.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Miktar zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Miktar 1'den büyük olmalıdır.")]
        public int Quantity { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public string? BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? StorageLocation { get; set; }
        public string? SupplierCode { get; set; }
        public string? SerialNumber { get; set; }
    }

}
