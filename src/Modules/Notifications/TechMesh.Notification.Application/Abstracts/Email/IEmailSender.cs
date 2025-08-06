namespace TechMesh.Notification.Application.Abstracts.Email;

public interface IEmailSender
{
    Task SendAsync(
        string nameTo,
        string to,
        string subject,
        string message,
        CancellationToken cancellationToken = default);
}