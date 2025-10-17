using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ShipmentCreateDto
    {
        // Gerekli ID’ler
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public int VehicleId { get; set; }

        // Sevkiyat bilgileri
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;

        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset EstimatedDeliveryDate { get; set; }

        // Opsiyonel: Sevkiyat durumu ve tracking number
        public string? Status { get; set; } // "Kargoda" gibi default olarak servis set edebilir
        public string? TrackingNumber { get; set; } // servis tarafından generate edilecek
    }
}
