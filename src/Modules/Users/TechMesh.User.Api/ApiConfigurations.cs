namespace TechMesh.User.Api;

public static class ApiConfigurations
{
    public static void AddCorsConfigurations(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("MyAllowSpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5297")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
}