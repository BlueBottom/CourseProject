namespace AdvertBoard.Contracts.Contexts.Categories;

/// <summary>
/// Модель запроса на обновление категории.
/// </summary>
public class UpdateCategoryDto
{
    /// <summary>
    /// Наименование категории.
    /// </summary>
    public string Title { get; set; }
}