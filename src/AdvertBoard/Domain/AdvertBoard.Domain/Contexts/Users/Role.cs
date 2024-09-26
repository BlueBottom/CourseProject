using AdvertBoard.Contracts.Enums;

namespace AdvertBoard.Domain.Contexts.Users;

/// <summary>
/// Роль пользователя.
/// </summary>
public class Role
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public UserRole Id { get; set; }
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }
}