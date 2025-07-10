namespace TechMesh.User.Application.Handlers.Notifications;

public class UserCreatedNotificationHandler : INotificationHandler<UserCreatedIntegrationEvent>
{
    private readonly IMessageBus _bus;

    public UserCreatedNotificationHandler(IMessageBus bus)
        => _bus = bus;

    public async Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        => await _bus.SendAsync(notification, cancellationToken);
}