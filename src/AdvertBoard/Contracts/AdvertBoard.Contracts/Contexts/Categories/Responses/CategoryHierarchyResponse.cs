namespace AdvertBoard.Contracts.Contexts.Categories.Responses;

/// <summary>
/// Категория.
/// </summary>
public class CategoryHierarchyResponse
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
    public IEnumerable<CategoryHierarchyResponse>? Children { get; set; }
}