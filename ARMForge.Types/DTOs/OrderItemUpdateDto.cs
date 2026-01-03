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
        [Range(1, int.MaxValue, ErrorMessage = "Miktar 1'den büyük olmalıdır.")]
        public int? Quantity { get; set; }

        [MaxLength(64)]
        public string? BatchNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [MaxLength(64)]
        public string? StorageLocation { get; set; }
    }
}
