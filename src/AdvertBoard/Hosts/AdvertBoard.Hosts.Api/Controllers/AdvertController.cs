using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using AdvertBoard.Contracts.Contexts.Adverts.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с объявлениями.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AdvertController : ControllerBase
{
    private readonly IAdvertService _advertService;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="advertService">Сервис для работы с объявлениями.</param>
    public AdvertController(IAdvertService advertService)
    {
        _advertService = advertService;
    }

    /// <summary>
    /// Получает каталог с укороченными моделями объявлений.
    /// </summary>
    /// <param name="getAdvertsByFilterRequest">Модель получения объявлений.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Каталог с укороченными моделями объявлений.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    [HttpPost("search")]
    [ProducesResponseType(typeof(PageResponse<ShortAdvertResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByFilterWithPaginationAsync(
        [FromQuery] GetAdvertsByFilterRequest getAdvertsByFilterRequest,
        CancellationToken cancellationToken)
    {
        var result = await _advertService.GetByFilterWithPaginationAsync(getAdvertsByFilterRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="createAdvertRequest">Модель создания объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddAsync([FromForm] CreateAdvertRequest createAdvertRequest,
        CancellationToken cancellationToken)
    {
        var result = await _advertService.AddAsync(createAdvertRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель объявления.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="404">Сущность не найдена.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AdvertResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет данные в объявлении.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateAdvertRequest">Модель обновления объвления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] UpdateAdvertRequest updateAdvertRequest,
        CancellationToken cancellationToken)
    {
        var result = await _advertService.UpdateAsync(id, updateAdvertRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="NoContentResult"/>.</returns>
    /// <response code="204">Контент отсутствует.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Меняет статус объявления на "Archived".
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмеы.</param>
    /// <returns><see cref="NoContentResult"/>.</returns>
    /// <response code="204">Контент отсутствует.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize]
    [HttpPatch("{id:guid}/archived")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ArchiveAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertService.ArchiveAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Меняет статус объявления на "Published".
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмеы.</param>
    /// <returns><see cref="NoContentResult"/>.</returns>
    /// <response code="204">Контент отсутствует.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize]
    [HttpPatch("{id:guid}/unarchived")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnarchiveAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertService.UnarchiveAsync(id, cancellationToken);
        return NoContent();
    }
}