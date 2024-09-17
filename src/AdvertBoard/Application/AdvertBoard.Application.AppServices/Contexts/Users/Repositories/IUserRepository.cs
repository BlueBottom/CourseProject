using System.Linq.Expressions;
using AdvertBoard.Contracts.Contexts.Users;
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
    /// <param name="registerUserDto">Модель регистрации.</param>
    /// <param name="password"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    public Task<Guid> AddUser(RegisterUserDto registerUserDto, string password, CancellationToken cancellationToken);
}