namespace AdvertBoard.Contracts.Contexts.Categories;

/// <summary>
/// Модель запроса создания категории.
/// </summary>
public class CreateCategoryDto
{
    /// <summary>
    /// Идентфиикатор родительской категории.
    /// </summary>
    public Guid? ParentId { get; set; }
    
    /// <summary>
    /// Наименование категории.
    /// </summary>
    public string Title { get; set; }
}