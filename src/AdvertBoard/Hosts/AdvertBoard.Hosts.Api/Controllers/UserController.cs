using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Contracts.Contexts.Users.Responses;
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
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
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
    /// <returns><see cref="NoContentResult"/>.</returns>
    /// <response code="204">Контент отсутствует.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
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
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
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
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("search")]
    [ProducesResponseType(typeof(PageResponse<ShortUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetByFilterWithPaginationAsync(
        [FromQuery] GetAllUsersByFilterRequest getAllUsersByFilterRequest,
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetByFilterWithPaginationAsync(getAllUsersByFilterRequest, cancellationToken);
        return Ok(result);
    }
}