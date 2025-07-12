using Event = TechMesh.Domain.Events.Event;

namespace TechMesh.Infrastructure.MessageBus;

public class MessageBus : IMessageBus
{
    private readonly IBus _bus;

    public MessageBus(IBus bus)
        => _bus = bus;

    public async Task SendAsync<TEvent>(TEvent @event, string address, CancellationToken cancellationToken)
        where TEvent : Event
    {
        var endpont = await _bus.GetSendEndpoint(new Uri(address));
        
        await endpont.Send(@event, cancellationToken);
    }


    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : Event
        => await _bus.Publish(@event, cancellationToken);
}