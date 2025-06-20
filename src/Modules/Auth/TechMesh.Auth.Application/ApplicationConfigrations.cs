namespace TechMesh.Auth.Application;

public static class ApplicationConfigrations
{
    public static void AddServicesConfigrations(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
    }
}