using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ShipmentCreateDto
    {
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public int VehicleId { get; set; }

        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;

        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset EstimatedDeliveryDate { get; set; }
    }
}
