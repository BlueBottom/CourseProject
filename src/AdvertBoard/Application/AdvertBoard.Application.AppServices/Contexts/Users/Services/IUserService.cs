using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Shared;

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
    public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет информацию пользователя.
    /// </summary>
    /// <param name="updateUserDto">Модель обновления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    public Task<Guid> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    public Task<bool> DeleteAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получает пользователей по фильтру.
    /// </summary>
    /// <param name="getAllUsersDto">Модель получения пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей пользователя.</returns>
    Task<PageResponse<ShortUserDto>> GetAllAsync(GetAllUsersDto getAllUsersDto,
        CancellationToken cancellationToken);
}