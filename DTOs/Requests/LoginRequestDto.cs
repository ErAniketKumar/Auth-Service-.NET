using System.ComponentModel.DataAnnotations;

namespace _NET_Practice_1.DTOs.Requests
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
