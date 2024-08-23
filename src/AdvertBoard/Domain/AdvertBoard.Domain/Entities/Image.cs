using AdvertBoard.Domain.Base;

namespace AdvertBoard.Domain.Entities;

/// <summary>
/// Изображение.
/// </summary>
public class Image : BaseEntity
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Содержимое.
    /// </summary>
    public Byte[] Content { get; set; }

    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
    
    /// <summary>
    /// Объявление.
    /// </summary>
    public Advert Advert { get; set; }
}