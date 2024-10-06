namespace AdvertBoard.Contracts.Contexts.Users.Responses;

/// <summary>
/// Пользователь.
/// </summary>
public class UserResponse
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
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string Phone { get; set; } = null!;

    /// <summary>
    /// Электронный почтовый адрес.
    /// </summary>
    public string Email { get; set; } = null!;
    
    /// <summary>
    /// Дата создания сущности.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}