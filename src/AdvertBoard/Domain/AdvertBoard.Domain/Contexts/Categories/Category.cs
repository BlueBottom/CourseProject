using AdvertBoard.Domain.Base;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Domain.Contexts.Categories;

/// <summary>
/// Категория.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Родительская категория.
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Родительская категория.
    /// </summary>
    public virtual Category? Parent { get; set; }
    
    /// <summary>
    /// Дочерние категории.
    /// </summary>
    public virtual ICollection<Category>? Childs { get; set; }

    /// <summary>
    /// Объяления.
    /// </summary>
    public virtual ICollection<Advert>? Adverts { get; set; }
}