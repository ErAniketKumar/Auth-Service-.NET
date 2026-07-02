using _NET_Practice_1.DTOs.Requests;
using _NET_Practice_1.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace _NET_Practice_1.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher;

        public PasswordService()
        {
            _hasher = new PasswordHasher<object>();
        }
        public string HashPassword(string password)
        {
            var hashedPassword = _hasher.HashPassword(null, password);
            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
