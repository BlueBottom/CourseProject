using System.Net;
using System.Net.Mail;

namespace AdvertBoard.Application.AppServices.Contexts.Email.Services;

/// <inheritdoc/>
public class UserEmailService : IUserEmailService
{
    public async Task SendEmailAboutRegistration(string name, string email, CancellationToken cancellationToken)
    {
        var smtpClient = new SmtpClient("smtp.yandex.ru")
        {
            Port = 587,
            Credentials = new NetworkCredential("AdvertBoardProject@yandex.ru", "cqskymofmcizczqr"),
            EnableSsl = true,
        };
        
        var mailMessage = new MailMessage
        {
            From = new MailAddress("AdvertBoardProject@yandex.ru"),
            Subject = "Добро пожаловать в AdvertBoard!",
            Body = $@"
        <h1>Здравствуйте, {name}!</h1>
        <p>Вы успешно зарегистрированы на платформе AdvertBoard. Мы рады приветствовать вас!</p>
        <p>Теперь вы можете размещать объявления, искать интересные предложения и пользоваться всеми функциями нашего сервиса.</p>
        <p>Если у вас возникнут вопросы, наша команда всегда готова помочь вам.</p>
        <p>Спасибо за ваш выбор!</p>
        <p>С уважением,<br>Команда AdvertBoard</p>",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
    }
}