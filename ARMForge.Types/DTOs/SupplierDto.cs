using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string CompanyName { get; set; } = string.Empty;

        [Required, MaxLength(128)]
        public string ContactPerson { get; set; } = string.Empty;

        [Required, MaxLength(320), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(32)]
        public string? PhoneNumber { get; set; }

        [Required, MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        public string? TaxId { get; set; }
    }
}
