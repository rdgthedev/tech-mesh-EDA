var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessageBusConfigurations(builder.Configuration);
builder.Services.AddEmailSenderConfigurations(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();