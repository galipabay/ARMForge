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

        // Shipment
        public string TrackingNumber { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset EstimatedDeliveryDate { get; set; }
        public DateTimeOffset? ActualDeliveryDate { get; set; }
        public string Status { get; set; } = string.Empty;

        // Driver
        public int DriverId { get; set; }
        public string? DriverFullName { get; set; }
        public bool IsDriverAvailable { get; set; }
        public bool IsDriverOnDuty { get; set; }

        // Vehicle
        public int VehicleId { get; set; }
        public string? VehiclePlate { get; set; }
        public bool IsVehicleAvailable { get; set; }

        // Order
        public int OrderId { get; set; }
        public string? OrderStatus { get; set; }
        public string? OrderNumber { get; set; }  // 🟢 Yeni alan eklendi

        // Audit
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

}
