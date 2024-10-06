namespace AdvertBoard.Contracts.Contexts.Reviews.Requests;

/// <summary>
/// Модель запроса на создание отзыва. 
/// </summary>
public class CreateReviewRequest
{
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// Пользователь, которому оставили отзыв.
    /// </summary>
    public Guid? ReceiverUserId { get; set; }
    
    /// <summary>
    /// Оценка пользователя.
    /// </summary>
    public int? Rating { get; set; }
}