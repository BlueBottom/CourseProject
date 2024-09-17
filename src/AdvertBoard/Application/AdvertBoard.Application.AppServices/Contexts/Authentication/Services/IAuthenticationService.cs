using AdvertBoard.Contracts.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Authentication.Services;

/// <summary>
/// Сервис регистрации и авторизации.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Регистрирует пользователя.
    /// </summary>
    /// <param name="registerUserDto">Модель регистрации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    public Task<Guid> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Логинит ползователя.
    /// </summary>
    /// <param name="loginUserDto">Модель логина.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<string> LoginAsync(LoginUserDto loginUserDto, CancellationToken cancellationToken);

}