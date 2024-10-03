using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Shared;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Repositories;

/// <summary>
/// Репозиторий пользователя.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Находит пользователя по предикату.
    /// </summary>
    /// <param name="predicate">Предикат.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Значение того, найден ли поьзователь.</returns>
    public Task<User?> FindUser(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет пользователя.
    /// </summary>
    /// <param name="user">Сущность пользователя при регистрации.</param>
    /// <param name="password"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    public Task<Guid> AddUser(User user, string password, CancellationToken cancellationToken);

    /// <summary>
    /// Получает данные о пользователе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель пользоватея.</returns>
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет данные о пользователе.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="updatedUser">Обновленный пользователь.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid userId, User updatedUser, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает всех пользователей по спецификации.
    /// </summary>
    /// <param name="specification">Спецификация.</param>
    /// <param name="paginationRequest">Модель запроса пагинации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Каталог укороченных моделей пользователя.</returns>
    Task<PageResponse<ShortUserDto>> GetAllByFilterWithPaginationAsync(ISpecification<User> specification,
        PaginationRequest paginationRequest,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновляет рейтинг пользователя на основе его отзывов.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="rating">Рейтинг.</param>
    /// <param name="cancellationToken">Токен  отмены.</param>
    Task UpdateRatingAsync(Guid id, decimal? rating, CancellationToken cancellationToken);
}