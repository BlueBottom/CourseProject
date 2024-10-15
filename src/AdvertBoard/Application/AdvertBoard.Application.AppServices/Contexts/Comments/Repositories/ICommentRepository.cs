using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Comments.Requests;
using AdvertBoard.Contracts.Contexts.Comments.Responses;
using AdvertBoard.Domain.Contexts.Comments;

namespace AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;

/// <summary>
/// Умный репозиторий для работы с комментариями.
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    /// Добавялет комментарий.
    /// </summary>
    /// <param name="comment">Комментарий.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> AddAsync(Comment comment, CancellationToken cancellationToken);

    /// <summary>
    /// Получает комментарии к объявлению, у которых нет родительских комментариев.
    /// </summary>
    /// <param name="getAllCommentsRequest">Модель запроса на получение комментариев.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей комментария с пагинацией.</returns>
    Task<PageResponse<ShortCommentResponse>> GetByAdvertWithPaginationAsync(GetAllCommentsRequest getAllCommentsRequest,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель комментария.</returns>
    Task<CommentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет комментарий.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateCommentRequest">Модель запроса на обновление комментария.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateCommentRequest updateCommentRequest, CancellationToken cancellationToken);
    
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
    Task<CommentHierarchyResponse> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет наличие комментария в БД.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Наличие комментария в БД.</returns>
    Task<bool> IsCommentExists(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет, принадлежит ли родительский комментарий к конкретному объявлению.
    /// </summary>
    /// <param name="parentId">Идентификатор родительского комментария.</param>
    /// <param name="advertId">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Соответсвие коментария объявлению.</returns>
    Task<bool> IsCurrentCommentRelatedToCurrentAdvert(Guid? parentId, Guid? advertId, CancellationToken cancellationToken);
}