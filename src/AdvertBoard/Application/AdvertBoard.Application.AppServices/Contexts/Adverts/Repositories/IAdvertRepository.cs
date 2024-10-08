using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using AdvertBoard.Contracts.Contexts.Adverts.Responses;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;

/// <summary>
/// Репозиторий.
/// </summary>
public interface IAdvertRepository
{
    /// <summary>
    /// Получает все объявления с использования спецификаций.
    /// </summary>
    /// <param name="paginationRequest"></param>
    /// <param name="specification">Спецификация.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей объявления с пагинацией.</returns>
    Task<PageResponse<ShortAdvertResponse>> GetByFilterWithPaginationAsync(PaginationRequest paginationRequest,
        ISpecification<Advert> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="advert"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор объявления.</returns>
    Task<Guid> AddAsync(Advert advert, CancellationToken cancellationToken);

    /// <summary>
    /// Обновлляет объявление.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updatedAdvert"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор объявления.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateAdvertRequest updatedAdvert, CancellationToken cancellationToken);
    
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
    /// Проверяет наличие и активность объявления в базе данных.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Наличие объявления в БД.</returns>
    Task<bool> IsAdvertExistsAndActive(Guid id, CancellationToken cancellationToken);

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