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
    Task SendReviewCreated(Guid reviewId, Guid receiverUserId, CancellationToken cancellationToken);

    /// <summary>
    /// Отправляет сообщение на электронную почту о регистрации пользователя.
    /// </summary>
    /// <param name="userName">Имя пользователя.</param>
    /// <param name="userEmail">Электронный почтовый адрес.</param>
    /// <param name="cancellationToken"></param>
    Task SendUserRegistered(string userName, string userEmail, CancellationToken cancellationToken);
}