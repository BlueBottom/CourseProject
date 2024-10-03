using AdvertBoard.Contracts.Contexts.Users;

namespace AdvertBoard.Contracts.Contexts.Comments;

/// <summary>
/// Комментарий с коллекцией дочерних элементов.
/// </summary>
public class CommentHierarchyDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Укороченная модель пользователя.
    /// </summary>
    public ShortUserDto User { get; set; } = null!;

    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; } = null!;
    
    /// <summary>
    /// Время создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Время редактирования.
    /// </summary>
    public DateTime? EditedAt { get; set; }
    
    /// <summary>
    /// Дочерние комментарии.
    /// </summary>
    public ICollection<CommentHierarchyDto>? Children { get; set; }
}