using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ARMForge.Kernel.Entities.Driver;

namespace ARMForge.Types.DTOs
{
    public class DriverUpdateDto
    {
        public bool IsOnDuty { get; set; }
        public DriverLicenseType LicenseType { get; set; }
        public bool IsAvailable { get; set; } // yeni alan eklendi
    }
}
