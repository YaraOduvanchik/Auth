namespace Auth.Domain;

public class RefreshSession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid Token { get; set; } = Guid.NewGuid();
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsUsed { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}