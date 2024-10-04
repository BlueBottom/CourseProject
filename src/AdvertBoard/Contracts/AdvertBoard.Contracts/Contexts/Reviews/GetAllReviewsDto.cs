using AdvertBoard.Contracts.Common;

namespace AdvertBoard.Contracts.Contexts.Reviews;

/// <summary>
/// Модель запроса на получение отзывов.
/// </summary>
public class GetAllReviewsDto : PaginationRequest
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
}