using AdvertBoard.Application.AppServices.Contexts.Images.Services;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Contracts.Contexts.Images.Requests;
using AdvertBoard.Hosts.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с изображениями.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="imageService">Сервис для работы с изображениями.</param>
    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    /// <summary>
    /// Получает изображение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель объявления.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _imageService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Добавляет изображение к объявлению.
    /// </summary>
    /// <param name="advertId">Идентификатор объявления.</param>
    /// <param name="file">Файл изображения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор изображения.</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync(Guid advertId, IFormFile file, CancellationToken cancellationToken)
    {
        var image = FormFileHelper.RequestFileToImage(file);
        var dto = new CreateImageRequest
        {
            AdvertId = advertId,
            File = image
        };
            var result = await _imageService.AddAsync(dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет изображение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _imageService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}