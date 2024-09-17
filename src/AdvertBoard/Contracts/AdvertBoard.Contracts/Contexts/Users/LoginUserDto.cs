namespace AdvertBoard.Contracts.Contexts.Users;

/// <summary>
/// Модель логина пользователя.
/// </summary>
public class LoginUserDto
{
    /// <summary>
    /// Электронный почтовый адрес.
    /// </summary>
    public string Email { get; set; } = null!;
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; } = null!;
}