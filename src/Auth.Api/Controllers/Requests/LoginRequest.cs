﻿namespace Auth.Api.Controllers.Requests;

public record LoginRequest(string Email, string Password);