using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Specifications;

/// <summary>
/// Спецификация поиска по минимальному рейтингу пользователя.
/// </summary>
public class MinRatingSpecification : Specification<User>
{
    private readonly decimal _minRating;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="minRating">Минимальный рейтинг поиска пользователей.</param>
    public MinRatingSpecification(decimal minRating)
    {
        _minRating = minRating;
    }

    public override Expression<Func<User, bool>> PredicateExpression => user =>
        user.Rating >= _minRating;
}