using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class VehicleCreateDto
    {
        [Required, MaxLength(16)]
        public string PlateNumber { get; set; } = string.Empty;

        [Required, MaxLength(64)]
        public string VehicleType { get; set; } = string.Empty;

        [Required, MaxLength(128)]
        public string Make { get; set; } = string.Empty;

        [Required, MaxLength(128)]
        public string Model { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int CapacityKg { get; set; }

        [Range(0, int.MaxValue)]
        public int CapacityM3 { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
