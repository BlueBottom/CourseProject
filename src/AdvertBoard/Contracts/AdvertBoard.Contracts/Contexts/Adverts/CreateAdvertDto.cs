using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Contracts.Contexts.Adverts;

/// <summary>
/// Модель для создания объявления. 
/// </summary>
public class CreateAdvertDto
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
    /// Регион.
    /// </summary>
    public string Location { get; set; } = null!;
    
    /// <summary>
    /// Адрес.
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }
    
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }
}