using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Reviews;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using AdvertBoard.Contracts.Contexts.Reviews.Responses;
using AdvertBoard.Domain.Contexts.Reviews;

namespace AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;

/// <summary>
/// Умный репозиторий для работы с отзывами.
/// </summary>
public interface IReviewRepository
{
    /// <summary>
    /// Получает отзывы на пользователя.
    /// </summary>
    /// <param name="getAllReviewsRequest">Модель запроса для получения пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей отзывов.</returns>
    Task<PageResponse<ShortReviewResponse>> GetAllByUserIdAsync(GetAllReviewsRequest getAllReviewsRequest,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает отзыв по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель отзыва.</returns>
    Task<ReviewResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Создает отзыв.
    /// </summary>
    /// <param name="review">Отзыв.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> AddAsync(Review review, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет отзыв.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedReviewRequest"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateReviewRequest updatedReviewRequest, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет отзыв.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Рассчитывает рейтинг пользователя по оставленным отзывам.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    Task<decimal?> CalcUserRatingAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет, оставлял ли уже пользователь отзыв другому конкретно пользователю.
    /// </summary>
    /// <param name="ownerUserId"></param>
    /// <param name="receiverUserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsUserAlreadyLeftReview(Guid ownerUserId, Guid receiverUserId, CancellationToken cancellationToken);
}