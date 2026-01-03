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
        [Required(ErrorMessage = "Sipariş ID zorunludur.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Ürün ID zorunludur.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Miktar zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Miktar 1'den büyük olmalıdır.")]
        public int Quantity { get; set; }

        // Siparişe özel lojistik değerler
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }

        // LOJİSTİK – OPSİYONEL, MANTIKLI
        [MaxLength(64)]
        public string? BatchNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [MaxLength(64)]
        public string? StorageLocation { get; set; }
    }
}
