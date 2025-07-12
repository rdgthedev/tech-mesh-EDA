namespace TechMesh.User.Application.Handlers.Notifications;

public class UserCreatedNotificationHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IMessageBus _bus;

    public UserCreatedNotificationHandler(IMessageBus bus)
        => _bus = bus;

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        => await _bus.SendAsync(Mapper.Map(notification), address: "queue:user-created", cancellationToken);
}