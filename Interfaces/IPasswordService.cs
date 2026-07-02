namespace _NET_Practice_1.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string password);

        bool VerifyPassword(string password, string hashedPassword);
    }
}
