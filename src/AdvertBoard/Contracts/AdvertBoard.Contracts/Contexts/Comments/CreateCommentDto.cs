namespace AdvertBoard.Contracts.Contexts.Comments;

/// <summary>
/// Модель запроса на создание комментария.
/// </summary>
public class CreateCommentDto
{
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
    
    /// <summary>
    /// Идентификатор родительского комментария.
    /// </summary>
    public Guid? ParentId { get; set; }
    
    /// <summary>
    /// Содержимое комментария.
    /// </summary>
    public string Content { get; set; } = null!;
}