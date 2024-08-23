using AdvertBoard.Domain.Base;

namespace AdvertBoard.Domain.Entities;

/// <summary>
/// Комментарий к объявлению.
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь. 
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
    
    /// <summary>
    /// Объявление.
    /// </summary>
    public Advert Advert { get; set; }
}