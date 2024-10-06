namespace AdvertBoard.Contracts.Contexts.Users.Requests;

/// <summary>
/// Модель регситрации пользователя.
/// </summary>
public class RegisterUserRequest
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
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string? Password { get; set; }
}