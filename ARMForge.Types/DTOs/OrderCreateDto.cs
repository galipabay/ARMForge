using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class OrderCreateDto
    {
        [Required(ErrorMessage = "Müşteri ID'si zorunludur.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Sipariş tarihi zorunludur.")]
        public DateTimeOffset OrderDate { get; set; }

        [Required(ErrorMessage = "Gerekli teslim tarihi zorunludur.")]
        public DateTimeOffset RequiredDate { get; set; }

        public DateTimeOffset? ShippedDate { get; set; } // Opsiyonel olabilir

        [MaxLength(64, ErrorMessage = "Durum alanı en fazla 64 karakter olabilir.")]
        public string Status { get; set; } = "Pending"; // Varsayılan değer

        [Required(ErrorMessage = "Toplam tutar zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Toplam tutar 0'dan büyük olmalıdır.")]
        public decimal TotalAmount { get; set; }

        // Sevkiyat bilgileri burada olmaz, ayrı bir endpoint'te yönetilir
        // public ICollection<Shipment> Shipments { get; set; }
    }
}
