using _NET_Practice_1.Enums;
using System.ComponentModel.DataAnnotations;

namespace _NET_Practice_1.DTOs.Requests
{
    public class RegisterRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsVerified { get; set; }
    }
}
