namespace AdvertBoard.Contracts.Enums;

/// <summary>
/// Перечисление ролей пользователя.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Обычный пользователь.
    /// </summary>
    User = 0,
    
    /// <summary>
    /// Модератор.
    /// </summary>
    Moderator = 1,
    
    /// <summary>
    /// Администратор.
    /// </summary>
    Admin = 2
}