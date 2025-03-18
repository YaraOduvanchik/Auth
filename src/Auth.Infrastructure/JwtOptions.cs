namespace Auth.Infrastructure;

public class JwtOptions
{
    public const string SECTION_NAME = "Jwt";
    
    public required string SecretKey { get; init; }
    public int AccessTokenLifetime { get; init; }
}