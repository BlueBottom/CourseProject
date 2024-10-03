namespace AdvertBoard.Contracts.Contexts.Comments;

/// <summary>
/// Модель запроса на создание комментария.
/// </summary>
public class UpdateCommentDto
{
    /// <summary>
    /// Содержимое комментария.
    /// </summary>
    public string Content { get; set; } = null!;
}