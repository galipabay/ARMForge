using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class VehicleDto
    {
        [MaxLength(16)]
        [Required]
        public string PlateNumber { get; set; } = string.Empty;

        [MaxLength(64)]
        [Required]
        public string VehicleType { get; set; } = string.Empty;

        [MaxLength(128)]
        [Required]
        public string Make { get; set; } = string.Empty;

        [MaxLength(128)]
        [Required]
        public string Model { get; set; } = string.Empty;

        public int CapacityKg { get; set; }
        public int CapacityM3 { get; set; } // Yeni eklendi
        public bool IsAvailable { get; set; }
    }
}
