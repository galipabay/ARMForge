using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class OrderUpdateDto
    {
        public int? CustomerId { get; set; }

        public DateTimeOffset? OrderDate { get; set; }
        public DateTimeOffset? RequiredDate { get; set; }
        public DateTimeOffset? ShippedDate { get; set; }

        [MaxLength(64, ErrorMessage = "Durum alanı en fazla 64 karakter olabilir.")]
        public string? Status { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Toplam tutar 0'dan büyük olmalıdır.")]
        public decimal? TotalAmount { get; set; }

        [MaxLength(32, ErrorMessage = "Sipariş numarası en fazla 32 karakter olabilir.")]
        public string? OrderNumber { get; set; }

        [MaxLength(32)]
        public string? PaymentMethod { get; set; }

        [MaxLength(500)]
        public string? DeliveryAddress { get; set; }
    }
}
