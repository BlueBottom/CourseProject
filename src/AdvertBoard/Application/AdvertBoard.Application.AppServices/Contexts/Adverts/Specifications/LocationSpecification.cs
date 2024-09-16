using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

public class LocationSpecification : Specification<Advert>
{
    private readonly int _location;

    public LocationSpecification(int location)
    {
        _location = location;
    }

    public override Expression<Func<Advert, bool>> PredicateExpression => advert => advert.Location == _location;
}