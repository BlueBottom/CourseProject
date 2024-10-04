namespace AdvertBoard.Contracts.Contexts.Users.Requests;

/// <summary>
/// Модель логина пользователя.
/// </summary>
public class LoginUserRequest
{
    /// <summary>
    /// Электронный почтовый адрес.
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string? Password { get; set; }
}