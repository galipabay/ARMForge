using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ShipmentInfoDto
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset EstimatedDeliveryDate { get; set; }
    }
}
