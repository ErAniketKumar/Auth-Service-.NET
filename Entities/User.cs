using _NET_Practice_1.Enums;
using System.ComponentModel.DataAnnotations;

namespace _NET_Practice_1.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password_hash { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public UserRole Role { get; set; } = UserRole.User;
    }
}
