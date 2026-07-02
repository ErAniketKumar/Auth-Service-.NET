namespace _NET_Practice_1.DTOs.Responses
{
    public class AuthResponseDto
    {
        public Guid UserId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Token { get; set; }

        public string? Message { get; set; }
    }
}
