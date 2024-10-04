namespace AdvertBoard.Contracts.Contexts.Images.Requests;

/// <summary>
/// Модель для добавления изображения.
/// </summary>
public class CreateImageRequest
{
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
    
    /// <summary>
    /// Изображение.
    /// </summary>
    public byte[] File { get; set; }
}