namespace AdvertBoard.Contracts.Contexts.Categories.Responses;

/// <summary>
/// Укороченная модель категории.
/// </summary>
public class ShortCategoryResponse
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