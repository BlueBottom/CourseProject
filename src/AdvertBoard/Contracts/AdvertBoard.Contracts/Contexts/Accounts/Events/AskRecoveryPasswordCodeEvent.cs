namespace AdvertBoard.Contracts.Contexts.Accounts.Events;

/// <summary>
/// Событие об отправки кода для восстановления пароля.
/// </summary>
public class AskRecoveryPasswordCodeEvent
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="AskRecoveryPasswordCodeEvent"/>.
    /// </summary>
    public AskRecoveryPasswordCodeEvent(string email, string code)
    {
        Email = email;
        Code = code;
    }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Код для восстановления пароля.
    /// </summary>
    public string Code { get; set; }
}