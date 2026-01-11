using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ARMForge.Kernel.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(64)]
        public string Firstname { get; set; } = null!;

        [Required, MaxLength(64)]
        public string Lastname { get; set; } = null!;

        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; } = null!;

        [MaxLength(32)]
        public string? PhoneNumber { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
        // HR alanları
        public DateTimeOffset? HireDate { get; set; }
        [MaxLength(64)]
        public string? Department { get; set; }
        public int? ManagerId { get; set; }
        public ICollection<User> DirectReports { get; set; } = [];
        public decimal Salary { get; set; }
        // 🔹 Many-to-many ilişki
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
