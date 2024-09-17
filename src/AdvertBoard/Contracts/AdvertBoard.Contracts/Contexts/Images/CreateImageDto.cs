using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Contracts.Contexts.Images;

/// <summary>
/// Модель для добавления изображения.
/// </summary>
public class CreateImageDto
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