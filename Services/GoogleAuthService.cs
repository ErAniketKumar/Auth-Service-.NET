using _NET_Practice_1.DTOs.Responses;
using _NET_Practice_1.Enums;
using _NET_Practice_1.Interfaces;
using Google.Apis.Auth;
using _NET_Practice_1.Entities;


namespace _NET_Practice_1.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IJwtService _jwtService;
        public GoogleAuthService(IAuthRepository repo, IJwtService jwtService) {
            _jwtService = jwtService;
            _repo = repo;
        }
        public async Task<AuthResponseDto> GoogleLoginAsync(string idToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            string name = payload.Name;
            string email = payload.Email;
            string picture = payload.Picture;

            var user = await _repo.GetByEmailAsync(email);

            if(user == null) {
                user = new User
                {
                    Name = name,
                    Email= email,
                    IsVerified = true,
                    Role =UserRole.User,
                };

               await _repo.CreateAsync(user);
            }
            var token = _jwtService.GetJwtToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token,
                Message = "Google login successful"
            };
        }
    }
}
