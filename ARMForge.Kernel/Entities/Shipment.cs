using ARMForge.Kernel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Todo: Add relationships to Order, Driver, and Vehicle entities
namespace ARMForge.Kernel.Entities
{
    public class Shipment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        [MaxLength(64)]
        public string TrackingNumber { get; set; } = string.Empty;

        [MaxLength(128)]
        public string? Origin { get; set; }

        [MaxLength(128)]
        public string? Destination { get; set; }

        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset EstimatedDeliveryDate { get; set; }
        public DateTimeOffset? ActualDeliveryDate { get; set; }

        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;

        // Opsiyonel ek alanlar
        public decimal? ShippingCost { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Volume { get; set; }
        public string? SpecialInstructions { get; set; }
    }
}
