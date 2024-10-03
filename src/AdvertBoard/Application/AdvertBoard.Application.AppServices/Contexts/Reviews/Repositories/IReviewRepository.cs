﻿using AdvertBoard.Contracts.Contexts.Reviews;
using AdvertBoard.Contracts.Shared;
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
    /// <param name="review">Отзыв.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> AddAsync(Review review, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет отзыв.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedReviewDto"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор.</returns>
    Task<Guid> UpdateAsync(Guid id, UpdateReviewDto updatedReviewDto, CancellationToken cancellationToken);
    
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
}