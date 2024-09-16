using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

public class MaxPriceSpecification : Specification<Advert>
{
    private readonly decimal _maxPrice;

    public MaxPriceSpecification(decimal maxPrice)
    {
        _maxPrice = maxPrice;
    }
    
    public override Expression<Func<Advert, bool>> PredicateExpression => advert => advert.Price <= _maxPrice;
}