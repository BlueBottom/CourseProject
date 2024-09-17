using AdvertBoard.Contracts.Contexts.Users;

namespace AdvertBoard.Contracts.Contexts.Reviews;

/// <summary>
/// Отзыв о пользователе.
/// </summary>
public class ReviewDto
{
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Идентификатор на пользователя, оставившего отзыв.
    /// </summary>
    public Guid OwnerUserId { get; set; }
    
    /// <summary>
    /// Пользователь, оставивший отзыв.
    /// </summary>
    public virtual UserDto? OwnerUser { get; set; }
    
    /// <summary>
    /// Пользователь, которому оставили отзыв.
    /// </summary>
    public Guid ReceiverUserId { get; set; }
    
    /// <summary>
    /// Идентификатор на пользователя, которому оставили отзыв.
    /// </summary>
    public virtual UserDto? ReceiverUser { get; set; }
}