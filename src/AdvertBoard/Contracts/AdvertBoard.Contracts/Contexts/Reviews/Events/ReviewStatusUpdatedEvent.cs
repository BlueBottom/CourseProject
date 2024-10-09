namespace AdvertBoard.Contracts.Contexts.Reviews.Events;

/// <summary>
/// Событие о создании отзыва.
/// </summary>
public class ReviewStatusUpdatedEvent
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ReviewStatusUpdatedEvent"/>.
    /// </summary>
    public ReviewStatusUpdatedEvent(Guid reviewId, Guid receiverUserId)
    {
        ReviewId = reviewId;
        ReceiverUserId = receiverUserId;
    }

    /// <summary>
    /// Идентификатор отзыва.
    /// </summary>
    public Guid ReviewId { get; }
    
    /// <summary>
    /// Идентификатор пользователя, на которого осавили отзыв.
    /// </summary>
    public Guid ReceiverUserId { get; }
}