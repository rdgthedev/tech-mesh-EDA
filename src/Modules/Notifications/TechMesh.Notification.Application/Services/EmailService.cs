
namespace TechMesh.Notification.Application.Services;

public class EmailService : IEmailService
{
    public async Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken)
    {
        Console.WriteLine("email sent successfully!");

        await Task.CompletedTask;
    }
}