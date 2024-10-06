using AdvertBoard.Contracts.Contexts.Users.Requests;

namespace AdvertBoard.Application.AppServices.Contexts.Authentication.Services;

/// <summary>
/// Сервис регистрации и авторизации.
/// </summary>
public interface IAuthenticationService
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
}