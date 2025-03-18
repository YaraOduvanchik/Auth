namespace Auth.Application.Interfaces;

public interface IPasswordHash
{
    string Generate(string password);
    bool Validate(string password, string passwordHash);
}