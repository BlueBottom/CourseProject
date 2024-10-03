using AdvertBoard.Application.AppServices.Contexts.Reviews.Services;
using AdvertBoard.Contracts.Contexts.Reviews;
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
    /// <param name="getAllReviewsDto">Модель запроса для получения пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей отзывов.</returns>
    [HttpGet("by-user")]
    public async Task<IActionResult> GetAllByUserIdAsync([FromQuery] GetAllReviewsDto getAllReviewsDto,
        CancellationToken cancellationToken)
    {
        var result = await _reviewService.GetAllByUserIdAsync(getAllReviewsDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает отзыв по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель отзыва.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reviewService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Создает отзыв.
    /// </summary>
    /// <param name="createReviewDto">Модель запроса на создание.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateReviewDto createReviewDto, CancellationToken cancellationToken)
    {
        var result = await _reviewService.AddAsync(createReviewDto, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Обновляет отзыв.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateReviewDto">Модель запроса на обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateReviewDto updateReviewDto,
        CancellationToken cancellationToken)
    {
        var result = await _reviewService.UpdateAsync(id, updateReviewDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет отзыв.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reviewService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}