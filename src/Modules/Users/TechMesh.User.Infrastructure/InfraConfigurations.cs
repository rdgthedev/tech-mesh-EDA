using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechMesh.Domain.Interfaces.UnitOfWork;
using TechMesh.Infrastructure.UnitOfWork;
using TechMesh.User.Domain.Interfaces.Repositories;
using TechMesh.User.Infrastructure.Context;
using TechMesh.User.Infrastructure.Persistence.Repositories;

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
    }

    public static void AddUnitOfWorkConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork<UserDbContext>>();
    }
}