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
}