using AdvertBoard.Domain.Base;
using AdvertBoard.Domain.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Domain.Contexts.Comments;

/// <summary>
/// Комментарий к объявлению.
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь. 
    /// </summary>
    public virtual User? User { get; set; }
    
    /// <summary>
    /// Идентификатор родительского комментария.
    /// </summary>
    public Guid? ParentId { get; set; }
    
    /// <summary>
    /// Родительский комментарий.
    /// </summary>
    public virtual Comment? Parent { get; set; }
    
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
    
    /// <summary>
    /// Объявление.
    /// </summary>
    public virtual Advert? Advert { get; set; }
    
    /// <summary>
    /// Дата последнего редактирования.
    /// </summary>
    public DateTime? EditedAt { get; set; }
    
    public virtual ICollection<Comment>? Children { get; set; }
}