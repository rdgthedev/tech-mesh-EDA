namespace TechMesh.User.Infrastructure.Messaging;

public class MessageBus : IMessageBus
{
    private readonly IBus _bus;

    public MessageBus(IBus bus)
        => _bus = bus;

    public async Task SendAsync(IntegrationEvent @event, CancellationToken cancellationToken)
        => await _bus.Send(@event, cancellationToken);

    public async Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken)
        => await _bus.Publish(@event, cancellationToken);
}