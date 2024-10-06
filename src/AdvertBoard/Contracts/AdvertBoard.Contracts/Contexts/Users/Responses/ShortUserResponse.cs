namespace AdvertBoard.Contracts.Contexts.Users.Responses;

/// <summary>
/// Укороченная модель пользователя.
/// </summary>
public class ShortUserResponse
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? Lastname { get; set; }
}