using _NET_Practice_1.DTOs.Requests;
using _NET_Practice_1.Entities;

namespace _NET_Practice_1.Interfaces
{
    public interface IJwtService
    {
        string GetJwtToken(User user);

    }
}
