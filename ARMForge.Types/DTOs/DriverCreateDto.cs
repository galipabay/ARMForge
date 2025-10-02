using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class DriverCreateDto
    {
        public int UserId { get; set; }
        public string LicenseType { get; set; }
        public bool IsOnDuty { get; set; }
        public DateTime? LastInspectionDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}
