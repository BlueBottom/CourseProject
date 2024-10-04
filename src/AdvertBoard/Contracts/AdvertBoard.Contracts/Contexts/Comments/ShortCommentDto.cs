using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Contexts.Users.Responses;

namespace AdvertBoard.Contracts.Contexts.Comments;

/// <summary>
/// Укороченная модель комментария.
/// </summary>
public class ShortCommentDto
{
    /// <summary>
    /// Идентификатор комментария.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Содержимое комментария.
    /// </summary>
    public string Content { get; set; } = null!;
    
    /// <summary>
    /// Укороченная модель пользователя.
    /// </summary>
    public ShortUserResponse User { get; set; } = null!;
    
    /// <summary>
    /// Время создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Время редактирования.
    /// </summary>
    public DateTime? EditedAt { get; set; }
    
    /// <summary>
    /// Количество дочерних элементов.
    /// </summary>
    public int ChildrenAmount { get; set; }
}