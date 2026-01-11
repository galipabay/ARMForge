using ARMForge.Kernel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ShipmentUpdateDto
    {
        [MaxLength(128)]
        public string? Origin { get; set; }

        [MaxLength(128)]
        public string? Destination { get; set; }

        public DateTimeOffset? DepartureDate { get; set; }
        public DateTimeOffset? EstimatedDeliveryDate { get; set; }
        public DateTimeOffset? ActualDeliveryDate { get; set; }

        public ShipmentStatus? Status { get; set; }

        public int? DriverId { get; set; }
        public int? VehicleId { get; set; }
    }
}
