namespace AdvertBoard.Contracts.Common;

/// <summary>
/// Модель ответа с использованием пагинации. 
/// </summary>
/// <typeparam name="T">Сущность, которая передается в ответе.</typeparam>
public class PageResponse<T>
{
    /// <summary>
    /// Коллекция сущностей типа <see cref="T"/>
    /// </summary>
    public IEnumerable<T>? Response { get; set; }
    
    /// <summary>
    /// Общее количество страниц.
    /// </summary>
    public int TotalPages { get; set; }
}