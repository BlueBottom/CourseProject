using AdvertBoard.Contracts.Shared;

namespace AdvertBoard.Contracts.Contexts.Adverts;

/// <summary>
/// Модель отображения доски объялений с фильтрами.
/// </summary>
public class GetAllAdvertsDto : PaginationRequest
{
    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Количество объявлений на страницу.
    /// </summary>
    public int BatchSize { get; set; } = 20;
    
    /// <summary>
    /// Строка поиска.
    /// </summary>
    public string? SearchString { get; set; }

    /// <summary>
    /// Минимальная цена.
    /// </summary>
    public decimal? MinPrice { get; set; }
    
    /// <summary>
    /// Максимальная цена.
    /// </summary>
    public decimal? MaxPrice { get; set; }
    
    /// <summary>
    /// Регион.
    /// </summary>
    public int? Location { get; set; }

    /// <summary>
    /// Опция "показывать не только активные объявления".
    /// </summary>
    public bool ShowNonActive { get; set; } = false;
    
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid? CategoryId { get; set; }
}