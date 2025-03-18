using Auth.Application.Interfaces;
using Auth.Domain;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistUserByEmail(string email, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }
    
    public Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task AddUser(User user, CancellationToken cancellationToken)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}