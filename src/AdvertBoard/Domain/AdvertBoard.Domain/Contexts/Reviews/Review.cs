using AdvertBoard.Contracts.Enums;
using AdvertBoard.Domain.Base;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Domain.Contexts.Reviews;

/// <summary>
/// Отзыв о пользователе.
/// </summary>
public class Review : BaseEntity
{
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; } = null!;
    
    /// <summary>
    /// Оценка пользователя.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Идентификатор на пользователя, оставившего отзыв.
    /// </summary>
    public Guid OwnerUserId { get; set; }
    
    /// <summary>
    /// Пользователь, оставивший отзыв.
    /// </summary>
    public virtual User? OwnerUser { get; set; }
    
    /// <summary>
    /// Пользователь, которому оставили отзыв.
    /// </summary>
    public Guid ReceiverUserId { get; set; }
    
    /// <summary>
    /// Идентификатор на пользователя, которому оставили отзыв.
    /// </summary>
    public virtual User? ReceiverUser { get; set; }
}