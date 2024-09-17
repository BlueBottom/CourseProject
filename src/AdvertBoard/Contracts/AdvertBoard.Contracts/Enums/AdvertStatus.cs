namespace AdvertBoard.Contracts.Enums;

/// <summary>
/// Статус объявления.
/// </summary>
public enum AdvertStatus
{
    /// <summary>
    /// Неопределенный.
    /// </summary>
    Undefined = 0,
    
    /// <summary>
    /// Черновик.
    /// </summary>
    Draft = 1,
    
    /// <summary>
    /// На модерации.
    /// </summary>
    InChecking = 2,
    
    /// <summary>
    /// Опубликовано.
    /// </summary>
    Published = 3,
    
    /// <summary>
    /// В архиве.
    /// </summary>
    Archived = 4,
}