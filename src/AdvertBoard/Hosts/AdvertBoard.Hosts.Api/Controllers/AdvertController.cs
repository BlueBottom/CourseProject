using System.Security.Claims;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Contracts.Contexts.Adverts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с объявлениями.
/// </summary>
[ApiController]
[Route("api/adverts")]
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
    /// <param name="getAllAdvertsDto">Модель получения объявлений.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Каталог с укороченными моделями объявлений.</returns>
    [HttpPost("search")]
    public async Task<IActionResult> GetAllAsync([FromForm] GetAllAdvertsDto getAllAdvertsDto, CancellationToken cancellationToken)
    {
        var result = await _advertService.GetByFilterAsync(getAllAdvertsDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="createAdvertDto">Модель создания объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromForm] CreateAdvertDto createAdvertDto,
        CancellationToken cancellationToken)
    {
        var result = await _advertService.AddAsync(createAdvertDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель объвления.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет данные в объявлении.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateAdvertDto">Модель обновления объвления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] UpdateAdvertDto updateAdvertDto,
        CancellationToken cancellationToken)
    {
        var result = await _advertService.UpdateAsync(id, updateAdvertDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия в виде <see cref="bool"/>.</returns>
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}