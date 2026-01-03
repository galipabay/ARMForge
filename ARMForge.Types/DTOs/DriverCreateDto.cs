using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class DriverCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public Driver.DriverLicenseType LicenseType { get; set; }

        public bool IsOnDuty { get; set; } = false;

        public DateTime? LastInspectionDate { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
