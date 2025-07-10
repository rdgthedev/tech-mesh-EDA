namespace TechMesh.User.Application.Abstracts.Messaging;

public interface IMessageBus
{
    Task SendAsync(IntegrationEvent message, CancellationToken cancellationToken);
    Task PublishAsync(IntegrationEvent message, CancellationToken cancellationToken);
}