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
        public int Id { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int CapacityKg { get; set; }
        public int CapacityM3 { get; set; }
        public bool IsAvailable { get; set; }
        public int ShipmentCount { get; set; }
    }
}
