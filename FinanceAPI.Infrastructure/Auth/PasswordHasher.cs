using FinanceAPI.Application.Interfaces;
using BCrypt.Net;

namespace FinanceAPI.Infrastructure.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
