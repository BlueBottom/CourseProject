using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Specifications;

/// <summary>
/// Спецификация поиска по начальной дате регистрации пользователя.
/// </summary>
public class StartDateSpecification : Specification<User>
{
    private readonly DateTime _startDate;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="startDate">Дата регистрации, от которой идет фильтрация.</param>
    public StartDateSpecification(DateTime startDate)
    {
        _startDate = startDate;
    }

    /// <inheritdoc/>
    public override Expression<Func<User, bool>> PredicateExpression => user =>
        user.CreatedAt >= _startDate;
}