using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using AdvertBoard.Contracts.Contexts.Adverts.Responses;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Services;

/// <summary>
/// Сервис.
/// </summary>
public interface IAdvertService
{
    /// <summary>
    /// Получает объявления с использованием фильтов.
    /// </summary>
    /// <param name="getAdvertsByFilterRequest">Модель полученя данных.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей объявления с пагинацией.</returns>
    Task<PageResponse<ShortAdvertResponse>> GetByFilterWithPaginationAsync(GetAdvertsByFilterRequest getAdvertsByFilterRequest,
        CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="createAdvertRequest"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор объявления.</returns>
    Task<Guid> AddAsync(CreateAdvertRequest createAdvertRequest, CancellationToken cancellationToken);
   
    /// <summary>
    /// Обновлляет объявление.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateAdvertRequest"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор объявления.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateAdvertRequest updateAdvertRequest, CancellationToken cancellationToken);
   
    /// <summary>
    /// Получает объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель объявления.</returns>
    Task<AdvertResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия в виде <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Меняет статус объявления на "Archived".
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмеы.</param>
    /// <returns>Статус действия в виде <see cref="bool"/>.</returns>
    Task<bool> ArchiveAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Меняет статус объявления на "Published".
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия в виде <see cref="bool"/>.</returns>
    Task<bool> UnarchiveAsync(Guid id, CancellationToken cancellationToken);
}