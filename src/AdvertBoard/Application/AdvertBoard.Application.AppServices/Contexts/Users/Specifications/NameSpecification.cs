using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Specifications;

/// <summary>
/// Спецификация поиска других пользователей по имени или фамилии.
/// </summary>
public class NameSpecification : Specification<User>
{
    private readonly string _searchString;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="searchString">Строка поиска.</param>
    public NameSpecification(string searchString)
    {
        _searchString = searchString;
    }

    /// <inheritdoc/>
    public override Expression<Func<User, bool>> PredicateExpression => user =>
        user.Name.ToLower().Contains(_searchString) ||
        user.Lastname.ToLower().Contains(_searchString); 
}