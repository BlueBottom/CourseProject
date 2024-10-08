using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Enums;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

/// <summary>
/// Спецификация активных объявлений.
/// </summary>
public class ActiveAdvertSpecification : Specification<Advert>
{
    /// <inheritdoc/>
    public override Expression<Func<Advert, bool>> PredicateExpression =>
        advert => advert.StatusId == AdvertStatus.Published;
}