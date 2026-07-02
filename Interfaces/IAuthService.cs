using _NET_Practice_1.DTOs.Requests;
using _NET_Practice_1.DTOs.Responses;
namespace _NET_Practice_1.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    }
}
