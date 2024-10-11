using System.Net;
using System.Net.Mail;
using AdvertBoard.Application.AppServices.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdvertBoard.Application.AppServices.Contexts.Email.Services;

/// <inheritdoc/>
public class UserEmailService : IUserEmailService
{
    private const string WelcomeText = "Contexts.Email.Resources.WelcomeUserMail.txt";
    
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserEmailService> _logger;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserEmailService"/>.
    /// </summary>
    public UserEmailService(IConfiguration configuration, ILogger<UserEmailService> logger)
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
    public async Task SendEmailAboutRegistration(string name, string email, CancellationToken cancellationToken)
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
}