using FinanceAPI.Domain.Common;

namespace FinanceAPI.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    protected User() { } // EF Core

    public User(string username, string email, string passwordHash)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        SetUpdated();
    }

    public void ChangeEmail(string email)
    {
        Email = email;
        SetUpdated();
    }
}