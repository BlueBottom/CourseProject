namespace AdvertBoard.Contracts.Contexts.Categories.Requests;

/// <summary>
/// Модель запроса на обновление категории.
/// </summary>
public class UpdateCategoryRequest
{
    /// <summary>
    /// Наименование категории.
    /// </summary>
    public string? Title { get; set; }
}