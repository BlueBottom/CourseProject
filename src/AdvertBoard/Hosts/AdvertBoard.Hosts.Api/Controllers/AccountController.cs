using AdvertBoard.Application.AppServices.Contexts.Accounts.Services;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с сервисом аккаунтов.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="accountService">Сервис для работы с аутентификацией.</param>
    /// <param name="logger">Логер.</param>
    public AccountController(IAccountService accountService,
        ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    /// <summary>
    /// Регистрирует пользователя.
    /// </summary>
    /// <param name="registerUserRequest">Модель регистрации пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор пользователя.</returns>
    /// <response code="202">Создание выполнено успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromForm] RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Регистрация пользователя по запросу");
        var result = await _accountService.RegisterAsync(registerUserRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Логинит пользователя.
    /// </summary>
    /// <param name="loginUserRequest">Модель логина.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>JWT токен.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginAsync([FromForm] LoginUserRequest loginUserRequest,
        CancellationToken cancellationToken)
    {
        var result = await _accountService.LoginAsync(loginUserRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Запрашивает код для восстановления пароля.
    /// </summary>
    /// <param name="request">Модель запроса.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    public async Task AskRecoveryPasswordCode(AskRecoveryPasswordCodeRequest request, CancellationToken cancellationToken)
    {
        await _accountService.AskRecoveryPasswordCode(request, cancellationToken);
    }

    /// <summary>
    /// Изменяет пароль неавторизованного пользователя.
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    [HttpPost("recover-password")]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    public async Task RecoverPasswordWithCode(RecoverPasswordWithCodeRequest request, CancellationToken cancellationToken)
    {
        await _accountService.RecoverPasswordWithCode(request, cancellationToken);
    }
}