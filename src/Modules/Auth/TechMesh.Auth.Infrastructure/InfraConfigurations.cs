namespace TechMesh.Auth.Infrastructure;

public static class InfraConfigurations
{
    public static void AddAuthenticationConfigurations(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("")),
                    ClockSkew = TimeSpan.Zero // evita tolerância de tempo
                };
            });
    }

    public static void AddAuthorizationConfigurations(this IServiceCollection services)
    {
        services.AddAuthorization();
    }

    public static void AddJwtOptionsConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Configuration.JwtOptions>(configuration.GetSection("JwtOptions"));
    }

    public static void AddDbConfirations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddInfraServicesConfigurations(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IJwtService, JwtService>();
    }

    public static void AddRepositoriesConfigurations(this IServiceCollection services)
    {
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }

    public static void AddUnitOfWorkConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
    }

    public static void AddRefitConfigrations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IUserServiceApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration["ExternalServices:UserService:BaseUrl"] ?? string.Empty))
            .AddTypedClient(c => RestService.For<IUserServiceApi>(c, new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                })
            }));
    }

    public static void AddAdaptersConfigurations(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasherAdapter, PasswordHasherAdapter>();
        services.AddScoped<IUserServiceApiRefitAdapter, UserServiceApiRefitAdapter>();
    }
}