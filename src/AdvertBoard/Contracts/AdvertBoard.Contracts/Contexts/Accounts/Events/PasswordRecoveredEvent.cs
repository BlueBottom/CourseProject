namespace AdvertBoard.Contracts.Contexts.Accounts.Events;

/// <summary>
/// Событие отправки письма об успешном восстановлении пароля.
/// </summary>
public class PasswordRecoveredEvent
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="PasswordRecoveredEvent"/>.
    /// </summary>
    public PasswordRecoveredEvent(string email)
    {
        Email = email;
    }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; }
}