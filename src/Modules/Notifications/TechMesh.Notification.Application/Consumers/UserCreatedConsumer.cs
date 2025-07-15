namespace TechMesh.Notification.Application.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreatedIntegrationEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(IEmailService emailService, ILogger<UserCreatedConsumer> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {
        var userCreated = context.Message;

        _logger.LogInformation("UserCreatedConsumer received event to user with email {email}", userCreated.Email);

        await _emailService.SendAsync(
            userCreated.Email,
            "Account Confirmation",
            $"Seu código de confirmação é: {Guid.NewGuid()}");
    }
}