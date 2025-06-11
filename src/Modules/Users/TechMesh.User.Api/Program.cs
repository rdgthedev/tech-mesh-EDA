using FluentValidation;
using TechMesh.User.Api;
using TechMesh.User.Application;
using TechMesh.User.Application.Validators.User;
using TechMesh.User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsConfigurations();
builder.Services.AddDbConfigurations(builder.Configuration);
builder.Services.AddValidatorsConfigurations();
builder.Services.AddMediatRConfigurations();

builder.Services.AddTransient<IUserFactory, UserFactory>();

builder.Services.AddRepositoriesConfigurations();
builder.Services.AddUnitOfWorkConfigurations();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyAllowSpecificOrigins");

app.MapControllers();

app.Run();