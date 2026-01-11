using ARMForge.Kernel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ShipmentCreateDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int DriverId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Origin { get; set; } = string.Empty;

        [Required]
        [MaxLength(128)]
        public string Destination { get; set; } = string.Empty;

        [Required]
        public DateTimeOffset DepartureDate { get; set; }

        [Required]
        public DateTimeOffset EstimatedDeliveryDate { get; set; }

    }
}
