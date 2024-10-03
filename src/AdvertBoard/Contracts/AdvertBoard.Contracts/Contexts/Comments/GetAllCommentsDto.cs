using AdvertBoard.Contracts.Shared;

namespace AdvertBoard.Contracts.Contexts.Comments;

/// <summary>
/// Модель запроса на получение объявлений.
/// </summary>
public class GetAllCommentsDto : PaginationRequest
{
    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid AdvertId { get; set; }
}