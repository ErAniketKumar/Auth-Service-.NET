using _NET_Practice_1.Data;
using _NET_Practice_1.Entities;
using _NET_Practice_1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _NET_Practice_1.Repositories
{
    public class AuthRepository:IAuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string? email)
        {
            var user = await _context.AccountUsers.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task CreateAsync(User user)
        {
            await _context.AccountUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
