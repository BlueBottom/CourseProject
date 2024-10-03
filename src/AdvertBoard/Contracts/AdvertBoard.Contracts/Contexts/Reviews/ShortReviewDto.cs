using AdvertBoard.Contracts.Contexts.Users;

namespace AdvertBoard.Contracts.Contexts.Reviews;

/// <summary>
/// Укороченная модель отзыва о пользователе.
/// </summary>
public class ShortReviewDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
     public Guid Id { get; set; }
    
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// Идентификатор на пользователя, оставившего отзыв.
    /// </summary>
    public ShortUserDto OwnerUser { get; set; } = null!;

    /// <summary>
    /// Оценка пользователя.
    /// </summary>
    public int Rating { get; set; }
}