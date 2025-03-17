using Auth.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Auth.Infrastructure;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IConfiguration _configuration;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<RefreshSession> RefreshSessions { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.Patronymic)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Entity<RefreshSession>()
            .HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}