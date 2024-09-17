namespace AdvertBoard.Contracts.Contexts.Users;

/// <summary>
/// Модель регситрации пользователя.
/// </summary>
public class RegisterUserDto
{
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
    /// Пароль.
    /// </summary>
    public string Password { get; set; } = null!;
}