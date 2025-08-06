namespace TechMesh.Notification.Application.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreatedIntegrationEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(IEmailSender emailSender, ILogger<UserCreatedConsumer> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {
        var userCreated = context.Message;

        _logger.LogInformation("UserCreatedConsumer received event to user with email: {email}", userCreated.Email);

        await _emailSender.SendAsync(
            userCreated.FullName,
            userCreated.Email,
            "Account Confirmation",
            $"Here is your confirmation token: {Guid.NewGuid()}");

        _logger.LogInformation("Email successfully sent to user with email: {email}", userCreated.Email);
    }
}