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
    /// <param name="receiverUserId"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SendReviewCreated(Guid reviewId, Guid receiverUserId, CancellationToken cancellationToken);
}