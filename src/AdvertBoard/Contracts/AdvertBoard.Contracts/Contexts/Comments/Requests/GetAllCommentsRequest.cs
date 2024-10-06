using AdvertBoard.Contracts.Common;

namespace AdvertBoard.Contracts.Contexts.Comments.Requests;

/// <summary>
/// Модель запроса на получение объявлений.
/// </summary>
public class GetAllCommentsRequest : PaginationRequest
{
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid? AdvertId { get; set; }
}