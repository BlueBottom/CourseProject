namespace AdvertBoard.Contracts.Contexts.Adverts.Requests;

/// <summary>
/// Модель для создания объявления. 
/// </summary>
public class CreateAdvertRequest
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal? Price { get; set; }
    
    /// <summary>
    /// Регион.
    /// </summary>
    public int? Location { get; set; }
    
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
    public Guid? CategoryId { get; set; }
}