using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class ShipmentUpdateDto
    {
        // Güncellenebilir shipment alanları
        public string? Status { get; set; }
        public string? Origin { get; set; }  // İstenirse başlangıç noktası değiştirilebilir
        public string? Destination { get; set; }
        public DateTimeOffset? DepartureDate { get; set; }
        public DateTimeOffset? EstimatedDeliveryDate { get; set; }
        public DateTimeOffset? ActualDeliveryDate { get; set; }

        // Opsiyonel olarak sürücü ve araç durumu değiştirilebilir
        public bool? IsDriverOnDuty { get; set; }
        public bool? IsDriverAvailable { get; set; }
        public bool? IsVehicleAvailable { get; set; }
    }

}
