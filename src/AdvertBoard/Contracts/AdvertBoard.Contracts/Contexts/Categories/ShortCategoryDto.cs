namespace AdvertBoard.Contracts.Contexts.Categories;

public class ShortCategoryDto
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