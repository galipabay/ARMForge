using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required, MaxLength(16)]
        public string PlateNumber { get; set; } = string.Empty;

        [Required, MaxLength(64)]
        public string VehicleType { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int CapacityKg { get; set; }

        [Range(0, int.MaxValue)]
        public int CapacityM3 { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Vehicle → Shipment ilişkisi
        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
