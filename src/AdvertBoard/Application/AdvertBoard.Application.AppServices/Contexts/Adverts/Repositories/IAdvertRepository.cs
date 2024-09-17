using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Adverts;
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
    /// <param name="specification">Спецификация.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Каталог укороченных моделей объявления.</returns>
    Task<IEnumerable<ShortAdvertDto>> GetAllAsync(ISpecification<Advert> specification,
        CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="createAdvertDto"></param>
    /// <param name="userId"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор объявления.</returns>
    Task<Guid> AddAsync(CreateAdvertDto createAdvertDto, Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Обновлляет объявление.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateAdvertDto"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор объявления.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель объявления.</returns>
    Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия в виде <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}