using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Contracts.Shared;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Services;

/// <summary>
/// Сервис.
/// </summary>
public interface IAdvertService
{
    /// <summary>
    /// Получает объявления с использованием фильтов.
    /// </summary>
    /// <param name="getAllAdvertsDto">Модель полученя данных.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей объявления с пагинацией.</returns>
    Task<PageResponse<ShortAdvertDto>> GetByFilterWithPaginationAsync(GetAllAdvertsDto getAllAdvertsDto,
        CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="createAdvertDto"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор объявления.</returns>
    Task<Guid> AddAsync(CreateAdvertDto createAdvertDto, CancellationToken cancellationToken);
   
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