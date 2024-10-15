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

    /// <inheritdoc/>
    public Task SendMailAboutRegistration(string name, string email, CancellationToken cancellationToken)
    {
        var subject = "Добро пожаловать в AdvertBoard!";
        var parameters = new object[] { name };
        var logMessage = $"Пользователю {name} на email {email} было отправлено письмо о регистрации";

        return SendEmailAsync(email, subject, WelcomeText, parameters, logMessage, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SendRecoveryPasswordCode(string email, string code, CancellationToken cancellationToken)
    {
        var subject = "Восстановление пароля на платформе AdvertBoard.";
        var parameters = new object[] { code };
        var logMessage = $"Пользователю с email {email} был отправлен код для восстановления пароля.";

        return SendEmailAsync(email, subject, RecoveryPasswordText, parameters, logMessage, cancellationToken);
    }

    /// <inheritdoc/>
    public Task SendMailAboutPasswordRecovering(string email, CancellationToken cancellationToken)
    {
        var subject = "Восстановлен пароль на платформе AdvertBoard";
        object[]? parameters = null;
        var logMessage = $"Пользователю с email {email} было отправлено письмо о смене (восстановлении) пароля.";

        return SendEmailAsync(email, subject, PasswordRecoveredText, parameters, logMessage, cancellationToken);
    }

    /// <summary>
    /// Отправляет письмо на электронную почту.
    /// </summary>
    /// <param name="toEmail">Почта получателя.</param>
    /// <param name="subject">Заголовок.</param>
    /// <param name="templatePath">Путь к ресурсу.</param>
    /// <param name="parameters">Параметры для интерполяции строкового содержимого ресурса.</param>
    /// <param name="logMessage">Сообщения для лога.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    private async Task SendEmailAsync(
        string toEmail,
        string subject,
        string templatePath,
        object[]? parameters,
        string logMessage,
        CancellationToken cancellationToken)
    {
        using var smtpClient = new SmtpClient(_configuration["Email:SMTP"])
        {
            Port = int.Parse(_configuration["Email:Port"]!),
            Credentials = new NetworkCredential(
                _configuration["Email:Sender"],
                _configuration["Email:Password"]),
            EnableSsl = true,
        };

        string mailBody;
        if (parameters != null && parameters.Length > 0)
        {
            var template = await EmbeddedResourceHelper.GetEmbeddedResourceAsString(templatePath, cancellationToken);
            mailBody = string.Format(template, parameters);
        }
        else mailBody = await EmbeddedResourceHelper.GetEmbeddedResourceAsString(templatePath, cancellationToken);

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Email:Sender"]!),
            Subject = subject,
            Body = mailBody,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        try
        {
            await smtpClient.SendMailAsync(mailMessage, cancellationToken);
            _logger.LogInformation(logMessage);
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, $"Ошибка при отправке письма на {toEmail}");
            throw;
        }
    }
}