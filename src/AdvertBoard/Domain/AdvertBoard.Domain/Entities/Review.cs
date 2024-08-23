using AdvertBoard.Domain.Base;

namespace AdvertBoard.Domain.Entities;

/// <summary>
/// Отзыв о пользователе.
/// </summary>
public class Review : BaseEntity
{
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Идентификатор на пользователя, оставившего отзыв.
    /// </summary>
    public Guid OwnerUserId { get; set; }
    
    /// <summary>
    /// Пользователь, оставивший отзыв.
    /// </summary>
    public User OwnerUser { get; set; }
    
    /// <summary>
    /// Пользователь, которому оставили отзыв.
    /// </summary>
    public Guid ReceiverUserId { get; set; }
    
    /// <summary>
    /// Идентификатор на пользователя, которому оставили отзыв.
    /// </summary>
    public User ReceiverUser { get; set; }
}