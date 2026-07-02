using _NET_Practice_1.Interfaces;
using _NET_Practice_1.Entities;
using _NET_Practice_1.DTOs.Responses;
using _NET_Practice_1.DTOs.Requests;
namespace _NET_Practice_1.Services
{
    public class AuthService:IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        public AuthService(IAuthRepository repo, IPasswordService passwordService, IJwtService jwtService) {
            _repo = repo;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            try
            {
                var existUser = await _repo.GetByEmailAsync(request.Email);
                if (existUser == null)
                {
                    return new AuthResponseDto
                    {
                        Message = "User not exist!"
                    };
                }
                if (!_passwordService.VerifyPassword(request.Password, existUser.Password_hash))
                {
                    return new AuthResponseDto
                    {
                        Message = "wrong password"
                    };
                }


                var token = _jwtService.GetJwtToken(existUser);


                return new AuthResponseDto
                {
                    UserId = existUser.Id,
                    Name = existUser.Name,
                    Email = existUser.Email,
                    Token = token,
                    Message = "Login successfully!",
                };
            }
            catch (Exception e)
            {
                return new AuthResponseDto
                {
                    Message = $"Somthing went wrong!, Error: {e.Message}"
                };
            }
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                var existUser = await _repo.GetByEmailAsync(request.Email);

                if (existUser != null)
                {
                    return new AuthResponseDto
                    {
                        Message = "User already exists"
                    };
                }

                string Password_hash = _passwordService.HashPassword(request.Password);

                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password_hash = Password_hash,
                    IsVerified = request.IsVerified,
                    Role = request.Role
                };

                await _repo.CreateAsync(user);

                return new AuthResponseDto
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Message = "User registered successfully"
                };

            }
            catch (Exception e)
            {
                return new AuthResponseDto
                {
                    Message = $"Somthing went wrong!, Error: {e.Message}"
                };
            }
        }

    }
}
