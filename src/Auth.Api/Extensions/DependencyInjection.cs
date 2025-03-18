using Auth.Application;
using Auth.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;

namespace Auth.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services
            .AddApplication()
            .AddInfrastructure(configuration);

        return services;
    }

    public static async Task<WebApplication> UseConfiguring(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            await app.ApplyMigrateDatabase();
        }

        app.UseHttpsRedirection();

        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // app.MapIdentityApi<User>(); - Для генерации стандартных эндпоинтов

        return app;
    }
}