using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertBoard.Contracts.Contexts.Categories;
using AdvertBoard.Contracts.Contexts.Categories.Requests;
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
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateCategoryRequest createCategoryRequest, CancellationToken cancellationToken)
    {
        var result = await _categoryService.AddAsync(createCategoryRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает все высшие категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей категории.</returns>
    [HttpGet("parents")]
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
    [HttpGet("{id:guid}/hierarchy")]
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
    [HttpGet("{id:guid}")]
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
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryRequest updateCategoryRequest,
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
    /// <returns><see cref="NoContentResult"/></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}