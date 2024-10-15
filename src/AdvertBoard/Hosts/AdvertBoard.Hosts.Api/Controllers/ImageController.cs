using AdvertBoard.Application.AppServices.Contexts.Images.Services;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Images.Requests;
using AdvertBoard.Contracts.Contexts.Images.Responses;
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
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="404">Сущность не найдена.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ImageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
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
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Модель данных не валидна.</response>
    /// <response code="401">Пользователь не авторизован.</response>
    /// <response code="403">Нет права доступа.</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationApiError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
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
        var result = await _imageService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}