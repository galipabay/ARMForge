using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Driver : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public enum DriverLicenseType
        {
            A,
            B,
            C,
            D,
            E,
            F
        }
        public DriverLicenseType LicenseType { get; set; }
        
        public bool IsOnDuty { get; set; }
        
        public DateTime? LastInspectionDate { get; set; }
        
        public bool IsAvailable { get; set; }

        public ICollection<Shipment>? Shipments { get; set; }
    }
}
