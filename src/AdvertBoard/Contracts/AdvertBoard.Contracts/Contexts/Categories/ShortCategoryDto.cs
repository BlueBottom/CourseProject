namespace AdvertBoard.Contracts.Contexts.Categories;

/// <summary>
/// Укороченная модель категории.
/// </summary>
public class ShortCategoryDto
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор родительской категории.
    /// </summary>
    public Guid ParentId { get; set; }
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; } = null!;
}