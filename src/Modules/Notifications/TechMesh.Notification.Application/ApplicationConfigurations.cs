using Microsoft.Extensions.DependencyInjection;
using TechMesh.Notification.Application.Services;

namespace TechMesh.Notification.Application;

public static class ApplicationConfigurations
{
    public static void AddApplicationServicesConfigurations(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
    }
}