using _NET_Practice_1.DTOs.Requests;
using _NET_Practice_1.Entities;
using _NET_Practice_1.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _NET_Practice_1.Services
{
    public class JwtService : IJwtService
    {
        public string GetJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cbe4d210dc8be59250210cf6016d59047e563b424b1020c84e14e73aff855ec7"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "ani-client",
                audience: "ani-backend",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: creds
               );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
