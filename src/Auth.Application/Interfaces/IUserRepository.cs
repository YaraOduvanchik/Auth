using Auth.Domain;

namespace Auth.Application.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistUserByEmail(string email, CancellationToken cancellationToken);

    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);

    Task AddUser(User user, CancellationToken cancellationToken);
}