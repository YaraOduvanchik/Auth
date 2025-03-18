using Auth.Application.Commands;
using Auth.Application.Interfaces;
using Auth.Domain;
using Microsoft.Extensions.Logging;

namespace Auth.Application;

public class UserService
{
    private readonly IPasswordHash _passwordHash;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IPasswordHash passwordHash,
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        ILogger<UserService> logger)
    {
        _passwordHash = passwordHash;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _logger = logger;
    }

    public async Task<string> RegisterUser(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var existUserByEmail = await _userRepository.ExistUserByEmail(command.Email, cancellationToken);
        if (existUserByEmail)
            return "User already exist";

        var passwordHash = _passwordHash.Generate(command.Password);

        var user = new User
        {
            UserName = command.UserName,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Patronymic = command.Patronymic,
            Email = command.Email,
            PasswordHash = passwordHash
        };

        await _userRepository.AddUser(user, cancellationToken);

        _logger.LogInformation("User with id: {id} registered", user.Id);

        return string.Empty;
    }

    public async Task<(string token, string error)> LoginUser(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(command.Email, cancellationToken);
        if (user is null)
            return (string.Empty, "User not found");
        
        var isValid = _passwordHash.Validate(command.Password, user.PasswordHash!);
        if (isValid == false)
            return (string.Empty, "Invalid password");
        
        var token = _jwtProvider.GenerateToken(user);
        
        return (token, string.Empty);
    }
}