using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }
    }

}
