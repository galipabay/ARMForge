using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class DriverDto
    {
        //// Audit ve soft delete alanları
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public bool IsActive { get; set; }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Driver.DriverLicenseType LicenseType { get; set; }
        public bool IsOnDuty { get; set; }
        public DateTime? LastInspectionDate { get; set; }
        public bool IsAvailable { get; set; }
        public int ShipmentCount { get; set; }
    }
}
