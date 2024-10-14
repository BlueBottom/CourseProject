namespace AdvertBoard.Contracts.Contexts.Accounts.Events;

/// <summary>
/// Событие о регистрации пользователя.
/// </summary>
public class UserRegisteredEvent
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserRegisteredEvent"/>.
    /// </summary>
    public UserRegisteredEvent(string name, string email)
    {
        Name = name;
        Email = email;
    }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string Email { get; }
}