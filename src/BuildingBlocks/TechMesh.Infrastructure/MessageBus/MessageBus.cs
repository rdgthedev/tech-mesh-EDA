namespace TechMesh.Infrastructure.MessageBus;

public class MessageBus : IMessageBus
{
    private readonly IBus _bus;

    public MessageBus(IBus bus)
        => _bus = bus;

    public async Task SendAsync<TEvent>(
        TEvent @event,
        string address,
        CancellationToken cancellationToken) where TEvent : IntegrationEvent
    {
        var endpont = await _bus.GetSendEndpoint(new Uri($"queue:{address}"));

        await endpont.Send(@event, cancellationToken);
    }

    public async Task PublishAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken) where TEvent : IntegrationEvent
        => await _bus.Publish(@event, cancellationToken);
}