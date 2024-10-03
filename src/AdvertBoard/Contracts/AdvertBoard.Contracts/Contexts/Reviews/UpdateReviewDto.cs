namespace AdvertBoard.Contracts.Contexts.Reviews;

/// <summary>
/// Модель запроса на обновление отзыва.
/// </summary>
public class UpdateReviewDto
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