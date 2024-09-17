using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

/// <summary>
/// Спецификация минимальной допустимой цены.
/// </summary>
public class MinPriceSpecification : Specification<Advert>
{
    private readonly decimal _minPrice;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="minPrice">Минимальная цена.</param>
    public MinPriceSpecification(decimal minPrice)
    {
        _minPrice = minPrice;
    }

    /// <inheritdoc/>
    public override Expression<Func<Advert, bool>> PredicateExpression => advert => advert.Price >= _minPrice;
}