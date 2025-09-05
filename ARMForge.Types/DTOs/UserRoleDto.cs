using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class UserRoleDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }

        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
