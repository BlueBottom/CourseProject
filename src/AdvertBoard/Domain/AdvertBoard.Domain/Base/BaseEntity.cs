namespace AdvertBoard.Domain.Base;

/// <summary>
/// Базовый класс для всех сущностей.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата создания сущности.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}