using AdvertBoard.Domain.Base;

namespace AdvertBoard.Domain.Entities;

/// <summary>
/// Категория.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Родительская категория.
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// Родительская категория.
    /// </summary>
    public Category Parent { get; set; }
    
    /// <summary>
    /// Дочерние категории.
    /// </summary>
    public ICollection<Category> Childs { get; set; }

    /// <summary>
    /// Объяления.
    /// </summary>
    public ICollection<Advert> Adverts { get; set; }
}