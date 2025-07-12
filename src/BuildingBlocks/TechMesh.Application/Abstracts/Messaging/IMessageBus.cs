namespace TechMesh.Application.Abstracts.Messaging;

public interface IMessageBus
{
    Task SendAsync<TEvent>(TEvent @event, string address, CancellationToken cancellationToken) where TEvent : Event;
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : Event;
}