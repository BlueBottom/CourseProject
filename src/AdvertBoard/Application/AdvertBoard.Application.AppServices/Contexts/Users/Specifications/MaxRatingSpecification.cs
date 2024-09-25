using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Specifications;

/// <summary>
/// Спецификация поиска по минимальному рейтингу пользователя.
/// </summary>
public class MaxRatingSpecification : Specification<User>
{
    private readonly decimal _maxRating;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="maxRating">Максимальный рейтинг поика пошльзователей.</param>
    public MaxRatingSpecification(decimal maxRating)
    {
        _maxRating = maxRating;
    }

    public override Expression<Func<User, bool>> PredicateExpression => user =>
        user.Rating >= _maxRating;
}