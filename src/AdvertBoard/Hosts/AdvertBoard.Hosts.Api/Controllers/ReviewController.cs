using AdvertBoard.Application.AppServices.Contexts.Reviews.Services;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using AdvertBoard.Contracts.Contexts.Reviews.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с отзывами.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="reviewService">Сервис для работы с отзывами.</param>
    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>
    /// Получает отзывы на пользователя.
    /// </summary>
    /// <param name="getAllReviewsRequest">Модель запроса для получения пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей отзывов.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    [HttpGet("by-user")]
    [ProducesResponseType(typeof(PageResponse<ShortReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllByUserWithPaginationAsync(
        [FromQuery] GetAllReviewsRequest getAllReviewsRequest,
        CancellationToken cancellationToken)
    {
        var result = await _reviewService.GetAllByUsedWithPaginationAsync(getAllReviewsRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает отзыв по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель отзыва.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reviewService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Создает отзыв.
    /// </summary>
    /// <param name="createReviewRequest">Модель запроса на создание.</param>
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
    public async Task<IActionResult> AddAsync([FromForm] CreateReviewRequest createReviewRequest, CancellationToken cancellationToken)
    {
        var result = await _reviewService.AddAsync(createReviewRequest, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Обновляет отзыв.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateReviewRequest">Модель запроса на обновление.</param>
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
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] UpdateReviewRequest updateReviewRequest,
        CancellationToken cancellationToken)
    {
        var result = await _reviewService.UpdateAsync(id, updateReviewRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет отзыв.
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
        var result = await _reviewService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}