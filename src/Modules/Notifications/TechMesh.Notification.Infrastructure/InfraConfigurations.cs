using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace TechMesh.Notification.Infrastructure;

public static class InfraConfigurations
{
    public static void AddMessageBusConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["RabbitMQ:Host"] ?? string.Empty;
        var virtualHost = configuration["RabbitMQ:VirtualHost"] ?? string.Empty;
        var username = configuration["RabbitMQ:Username"] ?? string.Empty;
        var password = configuration["RabbitMQ:Password"] ?? string.Empty;

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumer<UserCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureJsonSerializerOptions(options =>
                {
                    options.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                    return options;
                });

                cfg.Host(host, virtualHost, h =>
                {
                    h.Username(username);
                    h.Password(password);
                });

                cfg.Message<UserCreatedIntegrationEvent>(cfgMsg
                    => cfgMsg.SetEntityName("user-created-exchange"));

                cfg.ReceiveEndpoint("user-created.notification-service",
                    e =>
                    {
                        e.ConfigureConsumer<UserCreatedConsumer>(context);

                        e.Bind("user-created-exchange",
                            bindCfg => bindCfg.ExchangeType = ExchangeType.Fanout);
                    });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}