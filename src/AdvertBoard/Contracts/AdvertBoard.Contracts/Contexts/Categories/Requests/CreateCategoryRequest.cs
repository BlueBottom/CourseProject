namespace AdvertBoard.Contracts.Contexts.Categories.Requests;

/// <summary>
/// Модель запроса создания категории.
/// </summary>
public class CreateCategoryRequest
{
    /// <summary>
    /// Идентфиикатор родительской категории.
    /// </summary>
    public Guid? ParentId { get; set; }
    
    /// <summary>
    /// Наименование категории.
    /// </summary>
    public string? Title { get; set; }
}