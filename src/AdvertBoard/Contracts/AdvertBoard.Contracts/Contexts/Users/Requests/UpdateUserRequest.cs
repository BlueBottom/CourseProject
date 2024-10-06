namespace AdvertBoard.Contracts.Contexts.Users.Requests;

/// <summary>
/// Модель запроса на обновление пользователя.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? Lastname { get; set; }
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Электронный почтовый адрес.
    /// </summary>
    public string? Email { get; set; }
}