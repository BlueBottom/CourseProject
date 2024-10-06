using System.Security.Claims;
using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с пользователями.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Инициализирует экзампляр класса.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Обновляет данные пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateUserRequest">Модель обновления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] UpdateUserRequest updateUserRequest,
        CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateAsync(id, updateUserRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="NoContentResult"/></returns>
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель пользователя.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает пользователей по фильтру.
    /// </summary>
    /// <param name="getAllUsersByFilterRequest">Модель получения пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей пользователя.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("search")]
    public async Task<IActionResult> GetAllAsync([FromForm] GetAllUsersByFilterRequest getAllUsersByFilterRequest,
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllByFilterWithPaginationAsync(getAllUsersByFilterRequest, cancellationToken);
        return Ok(result);
    }
}