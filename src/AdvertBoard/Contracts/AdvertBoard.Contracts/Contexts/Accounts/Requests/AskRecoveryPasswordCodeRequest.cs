namespace AdvertBoard.Contracts.Contexts.Accounts.Requests;

/// <summary>
/// Модель запрса на восстановление пароля.
/// </summary>
public class AskRecoveryPasswordCodeRequest
{
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string? Email { get; set; }
}