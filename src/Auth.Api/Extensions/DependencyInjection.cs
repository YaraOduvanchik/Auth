using Auth.Infrastructure;

namespace Auth.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen();

        services.AddInfrastructure(configuration);

        return services;
    }
}