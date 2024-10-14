namespace AdvertBoard.Application.AppServices.Contexts.Email.Services;

/// <summary>
/// Сервис для рассылки сообщений пользователям.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Отправляет сообщение на почту об успешной регистрации.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    /// <param name="email">Адрес электронной почты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SendMailAboutRegistration(string name, string email, CancellationToken cancellationToken);

    /// <summary>
    /// Отправляет код для восстановления пароля.
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    /// <param name="code">Код.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SendRecoveryPasswordCode(string email, string code, CancellationToken cancellationToken);

    /// <summary>
    /// Отправляет письмо об успешном восстановлении пароля.
    /// </summary>
    /// <param name="email">"Электронная почта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SendMailAboutPasswordRecovering(string email, CancellationToken cancellationToken);
}