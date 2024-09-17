namespace AdvertBoard.Contracts.Contexts.Categories;

/// <summary>
/// Категория.
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; } = null!;
}