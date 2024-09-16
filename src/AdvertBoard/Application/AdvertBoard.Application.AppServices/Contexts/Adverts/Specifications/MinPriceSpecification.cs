using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

public class MinPriceSpecification : Specification<Advert>
{
    private readonly decimal _minPrice;

    public MinPriceSpecification(decimal minPrice)
    {
        _minPrice = minPrice;
    }

    public override Expression<Func<Advert, bool>> PredicateExpression => advert => advert.Price >= _minPrice;
}