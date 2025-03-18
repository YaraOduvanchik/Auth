namespace Auth.Application.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string? Patronymic,
    string UserName,
    string Email,
    string Password);