using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Categories.Requests;
using AdvertBoard.Contracts.Contexts.Categories.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с категориями.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CategoryController"/>.
    /// </summary>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Добавляет категорию.
    /// </summary>
    /// <param name="createCategoryRequest">Модель запроса на создание категории.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddAsync([FromForm] CreateCategoryRequest createCategoryRequest, CancellationToken cancellationToken)
    {
        var result = await _categoryService.AddAsync(createCategoryRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает все высшие категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей категории.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    [HttpGet("parents")]
    [ProducesResponseType(typeof(IEnumerable<ShortCategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllParentsAsync(CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAllParentsAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает все подкатегории выбранной категории.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей категории.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="404">Сущность не найдена.</response>
    [HttpGet("{id:guid}/hierarchy")]
    [ProducesResponseType(typeof(CategoryHierarchyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetHierarchyByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает выбранную категорию.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Укороченную модель категории.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ShortCategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await  _categoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет категорию.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateCategoryRequest">Модель запроса на обновление категории.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] UpdateCategoryRequest updateCategoryRequest,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateAsync(id, updateCategoryRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет категорию.
    /// </summary>
    /// <param name="id">Идентификатоор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><see cref="NoContentResult"/>.</returns>
    /// <response code="204">Контент отсутствует.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    /// <response code="404">Сущность не найдена.</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}