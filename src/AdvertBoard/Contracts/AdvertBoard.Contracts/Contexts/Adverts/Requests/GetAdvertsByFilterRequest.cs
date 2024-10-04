using AdvertBoard.Contracts.Common;

namespace AdvertBoard.Contracts.Contexts.Adverts.Requests;

/// <summary>
/// Модель отображения доски объялений с фильтрами.
/// </summary>
public class GetAdvertsByFilterRequest : PaginationRequest
{
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
    /// Идентификаторы категории.
    /// </summary>
    public IEnumerable<Guid>? CategoryIds { get; set; }
}