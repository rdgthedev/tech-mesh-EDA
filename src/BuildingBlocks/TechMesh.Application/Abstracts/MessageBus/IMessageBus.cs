using TechMesh.Application.Abstracts.Events;

namespace TechMesh.Application.Abstracts.MessageBus;

public interface IMessageBus
{
    Task SendAsync<TEvent>(TEvent @event, string address, CancellationToken cancellationToken)
        where TEvent : IntegrationEvent;

    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IntegrationEvent;
}