using AdvertBoard.Contracts.Contexts.Users;

namespace AdvertBoard.Contracts.Contexts.Reviews;

/// <summary>
/// Отзыв о пользователе.
/// </summary>
public class ReviewDto
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
    public Guid OwnerUserId { get; set; }
    
    /// <summary>
    /// Пользователь, которому оставили отзыв.
    /// </summary>
    public Guid ReceiverUserId { get; set; }
    
    /// <summary>
    /// Оценка пользователя.
    /// </summary>
    public int Rating { get; set; }
}