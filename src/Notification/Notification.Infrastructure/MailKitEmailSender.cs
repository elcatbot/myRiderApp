namespace myRideApp.Notification.Infrastructure;

public class MailKitEmailSender : IEmailSender
{
    private readonly SmtpClient _client;
    private readonly string _fromAddress;
    private readonly SmtpSettings _settings;
    private readonly ILogger<MailKitEmailSender> _logger;

    public MailKitEmailSender(IOptions<SmtpSettings> options, ILogger<MailKitEmailSender> logger)
    {
        _settings = options.Value;
        _fromAddress = _settings.From;

        _client = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            EnableSsl = true
        };
        
        _logger = logger;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var mail = new MailMessage(_fromAddress, to, subject, body)
        {
            IsBodyHtml = false
        };
        try
        {
            await _client.SendMailAsync(mail);
        }
        catch
        {
            throw;
        }
    }
}
