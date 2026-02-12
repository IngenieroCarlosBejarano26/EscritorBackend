namespace Escritores.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddProjectServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.SetIsOriginAllowed(_ => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();


            });
        });
        return services;
    }
}
