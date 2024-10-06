using AdvertBoard.Contracts.Common;

namespace AdvertBoard.Contracts.Contexts.Reviews.Requests;

/// <summary>
/// Модель запроса на получение отзывов.
/// </summary>
public class GetAllReviewsRequest : PaginationRequest
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
}