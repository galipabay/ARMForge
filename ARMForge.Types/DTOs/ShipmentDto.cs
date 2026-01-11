using ARMForge.Kernel.Enums;
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
        public string OrderNumber { get; set; } = string.Empty;

        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;

        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = string.Empty;

        public string TrackingNumber { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;

        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset EstimatedDeliveryDate { get; set; }
        public DateTimeOffset? ActualDeliveryDate { get; set; }

        public ShipmentStatus Status { get; set; }
    }

}
