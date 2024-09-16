using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Contracts.Contexts.Adverts;

/// <summary>
/// Запрос на создание объявления. 
/// </summary>
public class UpdateAdvertDto
    {
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Город.
    /// </summary>
    public string Location { get; set; } = null!;
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }
    
    /// <summary>
    /// Изображения.
    /// </summary>
    public IEnumerable<IFormFile>? Images { get; set; }
}