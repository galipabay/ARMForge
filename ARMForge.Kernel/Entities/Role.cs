using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class Role : BaseEntity
    {
        [MaxLength(64)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string Description { get; set; } = null!;
        public bool IsSystemRole { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
