

namespace TechMesh.User.Application;

public static class ApplicationConfigurations
{
    public static void AddMediatRConfigurations(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }

    public static void AddValidatorsConfigurations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CreateUserValidator).Assembly);
    }

    public static void AddDomainServicesConfigurations(this IServiceCollection services)
    {
        services
            .AddTransient<
                Domain.Interfaces.ICreateUserWithTechnologiesDomainService,
                Domain.DomainServices.CreateUserWithTechnologiesDomainService>();
    }
}