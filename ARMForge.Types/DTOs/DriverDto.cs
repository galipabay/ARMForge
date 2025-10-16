using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class DriverDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string LicenseType { get; set; }
        public bool IsOnDuty { get; set; }
        public DateTime? LastInspectionDate { get; set; }
        public bool IsAvailable { get; set; }
        // Audit ve soft delete alanları
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
