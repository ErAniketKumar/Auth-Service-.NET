using _NET_Practice_1.DTOs.Responses;

namespace _NET_Practice_1.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<AuthResponseDto> GoogleLoginAsync(string idToken);
    }
}
