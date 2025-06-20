using TechMesh.Auth.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddJwtOptionsConfigurations(builder.Configuration);
builder.Services.AddDbConfirations(builder.Configuration);
// builder.Services.AddAuthenticationConfigurations();
// builder.Services.AddAuthorizationConfigurations();
builder.Services.AddRefitConfigrations(builder.Configuration);
builder.Services.AddAdaptersConfigurations();
builder.Services.AddRepositoriesConfigurations();
builder.Services.AddUnitOfWorkConfigurations();
builder.Services.AddInfraServicesConfigurations();
builder.Services.AddServicesConfigrations();

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