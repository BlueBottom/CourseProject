namespace AdvertBoard.Contracts.Contexts.Images.Responses;

/// <summary>
/// Модель изображения.
/// </summary>
public class ImageResponse
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
    
    /// <summary>
    /// Содержимое.
    /// </summary>
    public byte[] Content { get; set; } = null!;
}