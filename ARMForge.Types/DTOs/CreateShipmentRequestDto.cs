using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class CreateShipmentRequestDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int DriverId { get; set; }

        [Required]
        public int VehicleId { get; set; }
    }
}
