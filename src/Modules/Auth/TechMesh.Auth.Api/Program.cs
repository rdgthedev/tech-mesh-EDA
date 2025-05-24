using Microsoft.EntityFrameworkCore;
using TechMesh.Api.Middlewares;
using TechMesh.Auth.Application.Adapters;
using TechMesh.Auth.Application.Adapters.Interfaces;
using TechMesh.Auth.Application.Interfaces.Services;
using TechMesh.Auth.Application.Services;
using TechMesh.Auth.Domain.Interfaces.Repositories;
using TechMesh.Auth.Infrastructure;
using TechMesh.Auth.Infrastructure.Contexts;
using TechMesh.Auth.Infrastructure.Persistence.Repositories;
using TechMesh.Auth.Infrastructure.Services.Auth;
using TechMesh.Auth.Infrastructure.Services.Token;
using TechMesh.Domain.Interfaces.UnitOfWork;
using TechMesh.Infrastructure.UnitOfWork;

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
//         options.RequireHttpsMetadata = true; // Mantenha true em produção
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

builder.Services.AddTransient<IPasswordHasherAdapter, PasswordHasherAdapter>();
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