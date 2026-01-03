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

        public DateTimeOffset? ShippedDate { get; set; }

        [MaxLength(64, ErrorMessage = "Durum alanı en fazla 64 karakter olabilir.")]
        public string Status { get; set; } = "Pending";

        [Required(ErrorMessage = "Toplam tutar zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Toplam tutar 0'dan büyük olmalıdır.")]
        public decimal TotalAmount { get; set; }

        // ✅ ORDER NUMBER EKLE
        [Required(ErrorMessage = "Sipariş numarası zorunludur.")]
        [MaxLength(32, ErrorMessage = "Sipariş numarası en fazla 32 karakter olabilir.")]
        public string OrderNumber { get; set; } = string.Empty;

        [MaxLength(32)]
        public string? PaymentMethod { get; set; }

        [MaxLength(32)]
        public string? PaymentStatus { get; set; }

        public DateTimeOffset? PaymentDate { get; set; }

        [Required, MaxLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string DeliveryCity { get; set; } = string.Empty;

        public List<OrderItemCreateDto> OrderItems { get; set; } = new();
    }
}
