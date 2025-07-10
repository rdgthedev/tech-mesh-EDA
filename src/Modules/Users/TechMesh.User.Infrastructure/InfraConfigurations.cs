namespace TechMesh.User.Infrastructure;

public static class InfraConfigurations
{
    public static void AddDbConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddRepositoriesConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITechnologyRepository, TechnologyRepository>();
    }

    public static void AddUnitOfWorkConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork<UserDbContext>>();
    }

    public static void AddMessageBusConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["RabbitMQ:Host"] ?? string.Empty;
        var virtualHost = configuration["RabbitMQ:VirtualHost"] ?? string.Empty;
        var username = configuration["RabbitMQ:Username"] ?? string.Empty;
        var password = configuration["RabbitMQ:Password"] ?? string.Empty;
        
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, virtualHost, h =>
                {
                    h.Username(username);
                    h.Password(password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IMessageBus, MessageBus>();
    }
}