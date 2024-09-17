using AdvertBoard.Contracts.Enums;

namespace AdvertBoard.Contracts.Contexts.Adverts;

/// <summary>
/// Укороченная модель объявления.
/// </summary>
public class ShortAdvertDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; } = null!;
    
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Статус.
    /// </summary>
    public AdvertStatus Status { get; set; }
    
    /// <summary>
    /// Регион.
    /// </summary>
    public int Location { get; set; }
    
    /// <summary>
    /// Заставка.
    /// </summary>
    public Guid? ImageId { get; set; }
}