using System.Net;
using System.Net.Mail;
using AdvertBoard.Application.AppServices.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdvertBoard.Application.AppServices.Contexts.Email.Services;

/// <inheritdoc/>
public class EmailService : IEmailService
{
    private const string WelcomeText = "Contexts.Email.Resources.WelcomeUserMail.txt";
    private const string RecoveryPasswordText = "Contexts.Email.Resources.PasswordRecoverMail.txt";
    private const string PasswordRecoveredText = "Contexts.Email.Resources.PasswordRecoveredInfoMail.txt";
    
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="EmailService"/>.
    /// </summary>
    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Отправляет сообщение на электронную почту об успешной регистрации.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    /// <param name="email">Электронный адрес.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task SendMailAboutRegistration(string name, string email, CancellationToken cancellationToken)
    {
        var smtpClient = new SmtpClient(_configuration["Email:SMTP"])
        {
            Port = int.Parse(_configuration["Email:Port"]!),
            Credentials = new NetworkCredential(_configuration["Email:Sender"], _configuration["Email:Password"]),
            EnableSsl = true,
        };

        var mailBody = await EmbeddedResourceHelper.GetEmbeddedResourceAsString(WelcomeText, cancellationToken);
        var format = string.Format(mailBody, name);

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Email:Sender"]!),
            Subject = "Добро пожаловать в AdvertBoard!",
            Body = format,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
        _logger.LogInformation($"Пользователю {name} на email {email} было отправлено пьсьмо о регистрации");
    }

    /// <summary>
    /// Отправляет на электронную почту код для восстановления пароля.
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    /// <param name="code">Код.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task SendRecoveryPasswordCode(string email, string code, CancellationToken cancellationToken)
    {
        var smtpClient = new SmtpClient(_configuration["Email:SMTP"])
        {
            Port = int.Parse(_configuration["Email:Port"]!),
            Credentials = new NetworkCredential(_configuration["Email:Sender"], _configuration["Email:Password"]),
            EnableSsl = true,
        };
        
        var mailBody = await EmbeddedResourceHelper.GetEmbeddedResourceAsString(RecoveryPasswordText, cancellationToken);
        var format = string.Format(mailBody, code);

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Email:Sender"]!),
            Subject = "Восстановление пароля на платформе AdvertBoard",
            Body = format,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
        _logger.LogInformation($"Пользователю с email {email} был отправлен код {code} для восстановления пароля.");
    }

    public async Task SendMailAboutPasswordRecovering(string email, CancellationToken cancellationToken)
    {
        var smtpClient = new SmtpClient(_configuration["Email:SMTP"])
        {
            Port = int.Parse(_configuration["Email:Port"]!),
            Credentials = new NetworkCredential(_configuration["Email:Sender"], _configuration["Email:Password"]),
            EnableSsl = true,
        };
        
        var mailBody = await EmbeddedResourceHelper.GetEmbeddedResourceAsString(PasswordRecoveredText, cancellationToken);

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Email:Sender"]!),
            Subject = "Восстановлен пароль на платформе AdvertBoard",
            Body = mailBody,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
        _logger.LogInformation($"Пользователю с email {email} было отправлено письмо о смене (восстановлении) пароля.");
    }
}