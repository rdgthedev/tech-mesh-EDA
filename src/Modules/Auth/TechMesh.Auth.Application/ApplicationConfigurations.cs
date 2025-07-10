using TechMesh.Auth.Application.Abstracts.Services.Auth;
using TechMesh.Auth.Application.Abstracts.Services.Role;
using TechMesh.Auth.Application.Abstracts.Services.User;

namespace TechMesh.Auth.Application;

public static class ApplicationConfigurations
{
    public static void AddServicesConfigrations(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
    }
}