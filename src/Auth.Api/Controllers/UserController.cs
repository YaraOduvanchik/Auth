using Auth.Api.Controllers.Requests;
using Auth.Application;
using Auth.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] UserService userService,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.UserName,
            request.Email,
            request.Password);

        string error = await userService.RegisterUser(command, cancellationToken);
        if (!string.IsNullOrWhiteSpace(error))
            return BadRequest(error);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] UserService userService,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);

        var (token, error) = await userService.LoginUser(command, cancellationToken);
        if (!string.IsNullOrWhiteSpace(error))
            return BadRequest(error);

        // TODO: Не безопасно токен хранить под конкретным названием access_token
        Response.Cookies.Append("access_token", token);

        return Ok();
    }

    [Authorize]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok();
    }
}