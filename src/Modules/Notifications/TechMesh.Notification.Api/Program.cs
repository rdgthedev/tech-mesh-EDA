using TechMesh.Notification.Application;
using TechMesh.Notification.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServicesConfigurations();
builder.Services.AddMessageBusConfigurations(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();