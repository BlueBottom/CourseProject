namespace AdvertBoard.Application.AppServices.Notifications.Services;

/// <summary>
/// Сервис для работы с уведомлениями.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Отправка уведомления о создании отзыва.
    /// </summary>
    /// <param name="reviewId">Идентификатор отзыва.</param>
    /// <param name="receiverUserId">Идентификатор получателя отзыва.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SendReviewStatusUpdated(Guid reviewId, Guid receiverUserId, CancellationToken cancellationToken);

    /// <summary>
    /// Отправляет сообщение на электронную почту о регистрации пользователя.
    /// </summary>
    /// <param name="userName">Имя пользователя.</param>
    /// <param name="userEmail">Электронный почтовый адрес.</param>
    /// <param name="cancellationToken"></param>
    Task SendUserRegistered(string userName, string userEmail, CancellationToken cancellationToken);

    /// <summary>
    /// Отправляет код на почту пользователя, необходимый для восстановления пароля.
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    /// <param name="code">Код для восстановления пароля.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SendPasswordRecoveryCode(string email, string code, CancellationToken cancellationToken);

    /// <summary>
    /// Отправляе письмо об успешном восстановлении пароля.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendPasswordSuccessfullyRecovered(string email, CancellationToken cancellationToken);
}