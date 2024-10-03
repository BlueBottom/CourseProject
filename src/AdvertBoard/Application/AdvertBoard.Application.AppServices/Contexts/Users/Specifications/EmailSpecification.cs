using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Specifications;

/// <summary>
/// Спецификация поиска других пользователей с заданным email.
/// </summary>
public class EmailSpecification : Specification<User>
{
    private readonly string _email;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="id"></param>
    public EmailSpecification(string email)
    {
        _email = email;
    }

    /// <inheritdoc/>
    public override Expression<Func<User, bool>> PredicateExpression => user =>
        user.Email.ToLower().Contains(_email);
}