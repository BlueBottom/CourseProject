using AdvertBoard.Contracts.Contexts.Reviews;
using AdvertBoard.Contracts.Shared;

namespace AdvertBoard.Application.AppServices.Contexts.Reviews.Services;

/// <summary>
/// Сервис для работы с отзывами.
/// </summary>
public interface IReviewService
{
    /// <summary>
    /// Получает отзывы на пользователя.
    /// </summary>
    /// <param name="getAllReviewsDto">Модель запроса для получения пользователей.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию укороченных моделей отзывов.</returns>
    Task<PageResponse<ShortReviewDto>> GetAllByUserIdAsync(GetAllReviewsDto getAllReviewsDto,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает отзыв по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель отзыва.</returns>
    Task<ReviewDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создает отзыв.
    /// </summary>
    /// <param name="createReviewDto">Модель запроса на создание.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> AddAsync(CreateReviewDto createReviewDto, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет отзыв.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="updateReviewDto">Модель запроса на обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateReviewDto updateReviewDto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет отзыв.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Статус действия типа <see cref="bool"/>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}