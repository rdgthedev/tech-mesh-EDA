namespace TechMesh.Notification.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private readonly Settings.EmailSettings _emailSettings;

    public EmailSender(IOptions<Settings.EmailSettings> emailSettings)
        => _emailSettings = emailSettings.Value;

    public async Task SendAsync(
        string nameTo,
        string emailTo,
        string subject,
        string message,
        CancellationToken cancellationToken = default)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress("Tech Mesh", _emailSettings.Username));
        mimeMessage.To.Add(new MailboxAddress(nameTo, emailTo));
        mimeMessage.Subject = subject;

        mimeMessage.Body = new TextPart("plain") { Text = $"Hello {nameTo}, {message}" };

        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, false, cancellationToken);
        await smtpClient.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password, cancellationToken);
        await smtpClient.SendAsync(mimeMessage, cancellationToken);
        await smtpClient.DisconnectAsync(true, cancellationToken);
    }
}