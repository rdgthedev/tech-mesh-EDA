using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TechMesh.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            context.Response.ContentType = "application/json";

            var result = Result<string>.Failure(context.Response.StatusCode, ex.Message);

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonSerializerOptions));
        }
    }
}