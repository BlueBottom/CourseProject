namespace AdvertBoard.Application.AppServices.Contexts.Email.Services;

/// <summary>
/// Сервис для рассылки сообщений пользователям.
/// </summary>
public interface IUserEmailService
{
    /// <summary>
    /// Отправляет сообщение на почту об успешной регистрации.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    /// <param name="email">Адрес электронной почты.</param>
    /// <param name="cancellationToken"></param>
    Task SendEmailAboutRegistration(string name, string email, CancellationToken cancellationToken);
}