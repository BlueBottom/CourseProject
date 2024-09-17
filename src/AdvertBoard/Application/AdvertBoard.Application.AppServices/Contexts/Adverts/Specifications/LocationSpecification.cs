using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

/// <summary>
/// Спецификация региона.
/// </summary>
public class LocationSpecification : Specification<Advert>
{
    private readonly int _location;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="location">Регион.</param>
    public LocationSpecification(int location)
    {
        _location = location;
    }

    /// <inheritdoc/>
    public override Expression<Func<Advert, bool>> PredicateExpression => advert => advert.Location == _location;
}