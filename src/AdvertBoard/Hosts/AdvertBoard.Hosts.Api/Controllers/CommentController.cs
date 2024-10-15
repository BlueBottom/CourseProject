using AdvertBoard.Application.AppServices.Contexts.Comments.Services;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Comments.Requests;
using AdvertBoard.Contracts.Contexts.Comments.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с комментариями.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CommentController"/>.
    /// </summary>
    /// <param name="commentService">Сервис для работы с комментариями.</param>
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>
    /// Добавялет комментарий.
    /// </summary>
    /// <param name="createCommentRequest">Модель запрса на создание комментария.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddAsync([FromForm] CreateCommentRequest createCommentRequest, CancellationToken cancellationToken)
    {
        var result = await _commentService.AddAsync(createCommentRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает комментарии к объявлению, у которых нет родительских комментариев.
    /// </summary>
    /// <param name="getAllCommentsRequest">Модель запроса на получение комментариев.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей комментария с пагинацией.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    [HttpGet("by-advert")]
    [ProducesResponseType(typeof(PageResponse<ShortCommentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByAdvertWithPaginationAsync(
        [FromQuery] GetAllCommentsRequest getAllCommentsRequest,
        CancellationToken cancellationToken)
    {
        var result = await _commentService.GetByAdvertWithPaginationAsync(getAllCommentsRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель комментария.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет комментарий.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateCommentRequest">Модель запроса на обновление комментария.</param>
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
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] UpdateCommentRequest updateCommentRequest,
        CancellationToken cancellationToken)
    {
        var result = await _commentService.UpdateAsync(id, updateCommentRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет комментарий.
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
        var result = await _commentService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Получает иерархию комментариев.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Иерархию комментариев</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    [HttpGet("{id:guid}/by-hierarchy")]
    [ProducesResponseType(typeof(PageResponse<CommentHierarchyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetHierarchyByIdAsync(id, cancellationToken);
        return Ok(result);
    }
}