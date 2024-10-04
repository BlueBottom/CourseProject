using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Services;

/// <summary>
/// Сервис для работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель пользователя.</returns>
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет информацию пользователя.
    /// </summary>
    /// <param name="updateUserDto">Модель обновления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid userId, UpdateUserDto updateUserDto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Получает пользователей по фильтру.
    /// </summary>
    /// <param name="getAllUsersDto">Модель получения пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей пользователя.</returns>
    Task<PageResponse<ShortUserDto>> GetAllByFilterWithPaginationAsync(GetAllUsersDto getAllUsersDto,
        CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет рейтинг пользователя на основе отзывов.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="rating">Рейтинг.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task UpdateRatingAsync(Guid id, decimal? rating, CancellationToken cancellationToken);
}