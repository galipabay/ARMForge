using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
        public DateTimeOffset? HireDate { get; set; }
        public string? Department { get; set; }
        public int? ManagerId { get; set; }
        public decimal Salary { get; set; }
        public ICollection<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
    }
}
