namespace AdvertBoard.Contracts.Shared;

/// <summary>
/// Модель запроса для пагинации.
/// </summary>
public class PaginationRequest
{
    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Количество объявлений на страницу.
    /// </summary>
    public int BatchSize { get; set; } = 20;
}