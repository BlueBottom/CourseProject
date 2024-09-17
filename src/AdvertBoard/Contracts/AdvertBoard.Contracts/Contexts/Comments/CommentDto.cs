namespace AdvertBoard.Contracts.Contexts.Comments;

/// <summary>
/// Комментарий к объявлению.
/// </summary>
public class CommentDto
{
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; } = null!;
}