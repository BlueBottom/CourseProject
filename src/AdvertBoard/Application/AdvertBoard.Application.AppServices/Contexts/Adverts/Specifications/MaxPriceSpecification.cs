using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

/// <summary>
/// Спецификация максимальной допустимой цены.
/// </summary>
public class MaxPriceSpecification : Specification<Advert>
{
    private readonly decimal _maxPrice;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="maxPrice">Максимальная цена.</param>
    public MaxPriceSpecification(decimal maxPrice)
    {
        _maxPrice = maxPrice;
    }

    /// <inheritdoc/>
    public override Expression<Func<Advert, bool>> PredicateExpression => advert => advert.Price <= _maxPrice;
}