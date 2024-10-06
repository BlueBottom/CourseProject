using AdvertBoard.Application.AppServices.Contexts.Authentication.Services;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с сервисом аутентификации.
/// </summary>
[ApiController]
[AllowAnonymous]
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
    /// <param name="registerUserRequest">Модель регистрации пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор пользователя.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromForm] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.RegisterAsync(registerUserRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Логинит пользователя.
    /// </summary>
    /// <param name="loginUserRequest">Модель логина.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>JWT токен.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromForm] LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.LoginAsync(loginUserRequest, cancellationToken);
        return Ok(result);
    }
}