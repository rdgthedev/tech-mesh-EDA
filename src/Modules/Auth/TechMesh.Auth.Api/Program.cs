var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.Configure<Configuration.JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = true;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//
//             ValidIssuer = "Issuer",
//             ValidAudience = "Audience",
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("")),
//             ClockSkew = TimeSpan.Zero // evita tolerância de tempo
//         };
//     });
//
// builder.Services.AddAuthorization();

builder.Services.AddRefitClient<IUserServiceApi>()
    .ConfigureHttpClient(c =>
        c.BaseAddress = new Uri(builder.Configuration["ExternalServices:UserService:BaseUrl"] ?? string.Empty))
    .AddTypedClient(c => RestService.For<IUserServiceApi>(c, new RefitSettings
    {
        ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        })
    }));


builder.Services.AddTransient<IPasswordHasherAdapter, PasswordHasherAdapter>();
builder.Services.AddScoped<IUserServiceApiRefitAdapter, UserServiceApiRefitAdapter>();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();