using _NET_Practice_1.DTOs.Requests;
using _NET_Practice_1.DTOs.Responses;
using _NET_Practice_1.Entities;
using _NET_Practice_1.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace _NET_Practice_1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IDatabase _redisdb;

        public AuthService(
            IAuthRepository repo,
            IPasswordService passwordService,
            IJwtService jwtService,
            IConnectionMultiplexer redis)
        {
            _repo = repo;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _redisdb = redis.GetDatabase();
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            try
            {
                User? user = null;

                // 1. Check Redis
                var cache = await _redisdb.StringGetAsync($"user-email:{request.Email}");

                if (cache.HasValue)
                {
                    Console.WriteLine("User loaded from Redis");

                    user = JsonSerializer.Deserialize<User>(cache.ToString());
                    if (user == null)
                    {
                        return new AuthResponseDto
                        {
                            Message = "Failed to read user from Redis."
                        };
                    }
                }
                else
                {
                    Console.WriteLine("User loaded from SQL");

                    user = await _repo.GetByEmailAsync(request.Email);

                    if (user != null)
                    {
                        await _redisdb.StringSetAsync(
                            $"user-email:{request.Email}",
                            JsonSerializer.Serialize(user),
                            TimeSpan.FromMinutes(30));
                    }
                }

                // User not found
                if (user == null)
                {
                    return new AuthResponseDto
                    {
                        Message = "User not exist!"
                    };
                }

                // Password verification
                if (!_passwordService.VerifyPassword(request.Password, user.Password_hash))
                {
                    return new AuthResponseDto
                    {
                        Message = "Wrong Password!"
                    };
                }

                // Generate JWT
                var token = _jwtService.GetJwtToken(user);

                return new AuthResponseDto
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Token = token,
                    Message = "Login Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                Console.WriteLine("******** REDIS REGISTER CODE V2 ********");
                var existUser = await _repo.GetByEmailAsync(request.Email);

                if (existUser != null)
                {
                    return new AuthResponseDto
                    {
                        Message = "User already exists."
                    };
                }

                string passwordHash = _passwordService.HashPassword(request.Password);

                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password_hash = passwordHash,
                    Role = request.Role,
                    IsVerified = request.IsVerified
                };

                // Save in SQL
                await _repo.CreateAsync(user);

                // Save in Redis
                bool saved = await _redisdb.StringSetAsync(
                   $"user-email:{user.Email}",
                   JsonSerializer.Serialize(user),
                   TimeSpan.FromMinutes(30));

                Console.WriteLine($"Redis Saved = {saved}");

                return new AuthResponseDto
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Message = "User Registered Successfully!"
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Message = ex.Message
                };
            }
        }
    }
}