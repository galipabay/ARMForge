using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(64)]
        public string Firstname { get; set; }
        [MaxLength(64)]
        public string Lastname { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        [MaxLength(320)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(32)]
        public string? PhoneNumber { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
        // HR alanları
        public DateTimeOffset? HireDate { get; set; }
        [MaxLength(64)]
        public string? Department { get; set; }
        public int? ManagerId { get; set; }
        public ICollection<User> DirectReports { get; set; } = new List<User>();
        public decimal Salary { get; set; }
        // 🔹 Many-to-many ilişki
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
