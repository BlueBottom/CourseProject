using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Enums;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

public class ActiveAdvertSpecification : Specification<Advert>
{
    private readonly bool _showNonActive;

    public ActiveAdvertSpecification(bool showNonActive)
    {
        _showNonActive = showNonActive;
    }

    public override Expression<Func<Advert, bool>> PredicateExpression =>
        advert => advert.Status == AdvertStatus.Published;
}
