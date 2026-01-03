using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ARMForge.Kernel.Entities.Driver;

namespace ARMForge.Types.DTOs
{
    public class DriverUpdateDto
    {
        [Required]
        public Driver.DriverLicenseType LicenseType { get; set; }

        public bool IsOnDuty { get; set; }

        public DateTime? LastInspectionDate { get; set; }

        public bool IsAvailable { get; set; }
    }
}
