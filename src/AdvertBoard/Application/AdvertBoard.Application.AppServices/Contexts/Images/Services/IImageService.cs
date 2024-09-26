using AdvertBoard.Contracts.Contexts.Images;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Images.Services;

/// <summary>
/// Сервис.
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Получает изображение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Содержимое изображения.</returns>
    public Task<ImageDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет изображение к объявлению.
    /// </summary>
    /// <param name="createImageDto"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор изображения.</returns>
    public Task<Guid> AddAsync(CreateImageDto createImageDto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет изображение.
    /// </summary>
    /// <param name="guid">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия в виде <see cref="bool"/>.</returns>
    public Task<bool> DeleteAsync(Guid guid, CancellationToken cancellationToken);
}