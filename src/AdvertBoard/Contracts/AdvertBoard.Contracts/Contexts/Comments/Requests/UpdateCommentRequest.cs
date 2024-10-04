namespace AdvertBoard.Contracts.Contexts.Comments.Requests;

/// <summary>
/// Модель запроса на создание комментария.
/// </summary>
public class UpdateCommentRequest
{
    /// <summary>
    /// Содержимое комментария.
    /// </summary>
    public string? Content { get; set; }
}