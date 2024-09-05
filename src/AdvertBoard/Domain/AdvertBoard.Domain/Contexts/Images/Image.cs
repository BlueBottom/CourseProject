using AdvertBoard.Domain.Base;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Domain.Contexts.Images;

/// <summary>
/// Изображение.
/// </summary>
public class Image : BaseEntity
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; } = null!;

    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
    
    /// <summary>
    /// Объявление.
    /// </summary>
    public virtual Advert? Advert { get; set; }
}