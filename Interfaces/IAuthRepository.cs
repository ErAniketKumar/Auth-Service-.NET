using _NET_Practice_1.Entities;

namespace _NET_Practice_1.Interfaces
{
    public interface IAuthRepository
    {
        Task CreateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
