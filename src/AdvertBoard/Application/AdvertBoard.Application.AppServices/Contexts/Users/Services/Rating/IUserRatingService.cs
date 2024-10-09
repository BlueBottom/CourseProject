namespace AdvertBoard.Application.AppServices.Contexts.Users.Services.Rating;

/// <summary>
/// Сервис для изменения рейтинга пользователя.
/// </summary>
public interface IUserRatingService
{
    /// <summary>
    /// Считает рейтинг пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    Task EvaluateUserRatingAsync(Guid id, CancellationToken cancellationToken);
}