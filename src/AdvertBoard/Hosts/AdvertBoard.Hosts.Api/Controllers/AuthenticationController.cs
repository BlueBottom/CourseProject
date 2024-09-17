using AdvertBoard.Application.AppServices.Contexts.Authentication.Services;
using AdvertBoard.Contracts.Contexts.Users;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с сервисом аутентификации.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="authenticationService">Сервис для работы с аутентификацией.</param>
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Регистрирует пользователя.
    /// </summary>
    /// <param name="registerUserDto">Модель регистрации пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор пользователя.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.RegisterAsync(registerUserDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Логинит пользователя.
    /// </summary>
    /// <param name="loginUserDto">Модель логина.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>JWT токен.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.LoginAsync(loginUserDto, cancellationToken);
        return Ok(result);
    }
}