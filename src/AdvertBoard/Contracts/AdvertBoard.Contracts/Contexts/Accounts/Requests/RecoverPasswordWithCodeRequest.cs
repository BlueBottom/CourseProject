namespace AdvertBoard.Contracts.Contexts.Accounts.Requests;

/// <summary>
/// Запрос на изменение пароля путем его восстановления.
/// </summary>
public class RecoverPasswordWithCodeRequest
{
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Код для сброса пароля.
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Новый пароль.
    /// </summary>
    public string Password { get; set; }
}