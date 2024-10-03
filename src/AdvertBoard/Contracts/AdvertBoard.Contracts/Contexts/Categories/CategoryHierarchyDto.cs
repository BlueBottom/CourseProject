namespace AdvertBoard.Contracts.Contexts.Categories;

/// <summary>
/// Категория.
/// </summary>
public class CategoryHierarchyDto
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
    
    /// <summary>
    /// Дочерние категории.
    /// </summary>
    public IEnumerable<CategoryHierarchyDto>? Children { get; set; }
}