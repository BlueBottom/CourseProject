using AdvertBoard.Contracts.Enums;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Models;

/// <summary>
/// Внутренняя модель пользователя.
/// </summary>
public class UserWithPasswordModel
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string Email { get; set; } = null!;
    
    /// <summary>
    /// Роль.
    /// </summary>
    public UserRole RoleId { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string Phone { get; set; } = null!;

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; } = null!;
}