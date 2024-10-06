using AdvertBoard.Application.AppServices.Contexts.Users.Models;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Contracts.Contexts.Users.Responses;

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
    Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет информацию пользователя.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateUserRequest">Модель обновления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateUserRequest updateUserRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает пользователей по фильтру.
    /// </summary>
    /// <param name="getAllUsersByFilterRequest">Модель получения пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей пользователя.</returns>
    Task<PageResponse<ShortUserResponse>> GetAllByFilterWithPaginationAsync(GetAllUsersByFilterRequest getAllUsersByFilterRequest,
        CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет рейтинг пользователя на основе отзывов.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="rating">Рейтинг.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task UpdateRatingAsync(Guid id, decimal? rating, CancellationToken cancellationToken);
    
    
    /// <summary>
    /// Проводит поиск пользователя по электронному адресу.
    /// </summary>
    /// <param name="email">Адрес жлектронной почты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель для логина.</returns>
    public Task<UserWithPasswordModel?> FindByEmail(string email, CancellationToken cancellationToken);
    
    /// <summary>
    /// Проводит поиск пользователя с заданным номером телефона.
    /// </summary>
    /// <param name="phone">Номер телефона.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Наличие телефона в БД.</returns>
    public Task<bool> IsExistByPhone(string phone, CancellationToken cancellationToken);
}