using AdvertBoard.Contracts.Contexts.Accounts.Requests;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Services;

/// <summary>
/// Сервис для работы с аккаунтами.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Регистрирует пользователя.
    /// </summary>
    /// <param name="registerUserRequest">Модель регистрации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    public Task<Guid> RegisterAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken);
    
    /// <summary>
    /// Логинит ползователя.
    /// </summary>
    /// <param name="loginUserRequest">Модель логина.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<string> LoginAsync(LoginUserRequest loginUserRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Метод-запрос на восстановление пароля пользователя.
    /// </summary>
    /// <param name="request">"Модель запроса на восстановление пароля.</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task AskRecoveryPasswordCode(AskRecoveryPasswordCodeRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Восстанваливает пароль по коду из email.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task RecoverPasswordWithCode(RecoverPasswordWithCodeRequest request, CancellationToken cancellationToken);
}