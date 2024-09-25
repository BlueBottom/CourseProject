using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Builders;

/// <summary>
/// Создает спецификацию пользователя.
/// </summary>
public interface IUserSpecificationBuilder
{
    /// <summary>
    /// Строит спецификацию по модели.
    /// </summary>
    /// <returns>Спецификация.</returns>
    ISpecification<User> Build(GetAllUsersDto getAllUsersDto);
}