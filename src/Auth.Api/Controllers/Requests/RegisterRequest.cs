namespace Auth.Api.Controllers.Requests;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string UserName,
    string Email,
    string Password);