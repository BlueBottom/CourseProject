using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Specifications;

/// <summary>
/// Спецификация поиска по конечной дате регистрации пользователя.
/// </summary>
public class EndDateSpecification : Specification<User>
{
    private readonly DateTime _endDate;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="endDate">Дата регистрации, до которой идет фильтрация.</param>
    public EndDateSpecification(DateTime endDate)
    {
        _endDate = endDate;
    }

    /// <inheritdoc/>
    public override Expression<Func<User, bool>> PredicateExpression => user =>
        user.CreatedAt <= _endDate;
}