namespace AdvertBoard.Contracts.Contexts.Reviews.Requests;

/// <summary>
/// Модель запроса на обновление отзыва.
/// </summary>
public class UpdateReviewRequest
{
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// Оценка пользователя.
    /// </summary>
    public int Rating { get; set; }
}