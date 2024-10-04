using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Comments;

namespace AdvertBoard.Application.AppServices.Contexts.Comments.Services;

/// <summary>
/// Сервис для работы с комментариями.
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Добавялет комментарий.
    /// </summary>
    /// <param name="createCommentDto">Модель запрса на создание комментария.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> AddAsync(CreateCommentDto createCommentDto, CancellationToken cancellationToken);

    /// <summary>
    /// Получает комментарии к объявлению.
    /// </summary>
    /// <param name="getAllCommentsDto">Модель запроса на получение комментариев.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей комментария с пагинацией.</returns>
    Task<PageResponse<ShortCommentDto>> GetAllWithPaginationAsync(GetAllCommentsDto getAllCommentsDto,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель комментария.</returns>
    Task<CommentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет комментарий.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateCommentDto">Модель запроса на обновление комментария.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateCommentDto updateCommentDto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет комментарий.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает иерархию комментариев.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Иерархию комментариев</returns>
    Task<CommentHierarchyDto> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken);
}