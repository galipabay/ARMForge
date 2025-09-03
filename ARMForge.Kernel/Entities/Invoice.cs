using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Invoice : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public DateTimeOffset InvoiceDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalAmounts { get; set; }

        [MaxLength(32)]
        public string Status { get; set; } = string.Empty;
    }
}
