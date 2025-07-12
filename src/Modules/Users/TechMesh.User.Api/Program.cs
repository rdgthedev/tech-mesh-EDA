using TechMesh.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsConfigurations();
builder.Services.AddDbConfigurations(builder.Configuration);
builder.Services.AddValidatorsConfigurations();
builder.Services.AddMediatRConfigurations();
builder.Services.AddRepositoriesConfigurations();
builder.Services.AddUnitOfWorkConfigurations();
builder.Services.AddDomainServicesConfigurations();
builder.Services.AddFactoriesConfigurations();
builder.Services.AddMessageBusConfigurations(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyAllowSpecificOrigins");
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();