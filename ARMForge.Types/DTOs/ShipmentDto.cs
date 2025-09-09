using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ShipmentDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public int VehicleId { get; set; }

        [MaxLength(64)]
        public string TrackingNumber { get; set; } = string.Empty;

        [MaxLength(128)]
        public string Origin { get; set; } = string.Empty;

        [MaxLength(128)]
        public string Destination { get; set; } = string.Empty;

        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset EstimatedDeliveryDate { get; set; }
        public DateTimeOffset? ActualDeliveryDate { get; set; }

        [MaxLength(32)]
        public string Status { get; set; } = string.Empty;
    }
}
