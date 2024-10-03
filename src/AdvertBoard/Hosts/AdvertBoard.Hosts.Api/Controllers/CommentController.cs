using AdvertBoard.Application.AppServices.Contexts.Comments.Services;
using AdvertBoard.Contracts.Contexts.Comments;
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
    /// <param name="createCommentDto">Модель запрса на создание комментария.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] CreateCommentDto createCommentDto, CancellationToken cancellationToken)
    {
        var result = await _commentService.AddAsync(createCommentDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает комментарии к объявлению.
    /// </summary>
    /// <param name="getAllCommentsDto">Модель запроса на получение комментариев.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей комментария с пагинацией.</returns>
    [HttpGet("by-advert")]
    public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] GetAllCommentsDto getAllCommentsDto,
        CancellationToken cancellationToken)
    {
        var result = await _commentService.GetAllWithPaginationAsync(getAllCommentsDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель комментария.</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет комментарий.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateCommentDto">Модель запроса на обновление комментария.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCommentDto updateCommentDto,
        CancellationToken cancellationToken)
    {
        var result = await _commentService.UpdateAsync(id, updateCommentDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет комментарий.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    [Authorize]
    [HttpDelete("{id:guid}")]
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
    [HttpGet("{id:guid}/by-hierarchy")]
    public async Task<IActionResult> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetHierarchyByIdAsync(id, cancellationToken);
        return Ok(result);
    }
}