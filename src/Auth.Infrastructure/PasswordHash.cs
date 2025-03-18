using Auth.Application.Interfaces;

namespace Auth.Infrastructure;

public class PasswordHash : IPasswordHash
{
    public string Generate(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Validate(string password, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}