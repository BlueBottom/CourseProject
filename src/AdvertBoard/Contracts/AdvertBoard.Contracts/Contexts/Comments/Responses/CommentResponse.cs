namespace AdvertBoard.Contracts.Contexts.Comments.Responses;

/// <summary>
/// Комментарий к объявлению.
/// </summary>
public class CommentResponse
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; } = null!;
    
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя, оставившего комментарий.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Время создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Время редактирования.
    /// </summary>
    public DateTime? EditedAt { get; set; }
}