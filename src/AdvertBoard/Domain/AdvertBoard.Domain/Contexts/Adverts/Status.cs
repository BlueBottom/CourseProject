using AdvertBoard.Contracts.Enums;

namespace AdvertBoard.Domain.Contexts.Adverts;

/// <summary>
/// Статус объявления.
/// </summary>
public class Status
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public AdvertStatus Id { get; set; }
    
    /// <summary>
    /// Наименование статуса.
    /// </summary>
    public string Name { get; set; } = null!;
}