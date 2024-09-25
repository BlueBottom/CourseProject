using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Specifications;

/// <summary>
/// Спецификация поиска других пользователей с заданным номером телефона.
/// </summary>
public class PhoneSpecification : Specification<User>
{
    private readonly string _phone;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="id"></param>
    public PhoneSpecification(string phone)
    {
        _phone = phone;
    }

    /// <inheritdoc/>
    public override Expression<Func<User, bool>> PredicateExpression => user => user.Phone == _phone;
}