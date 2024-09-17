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
    private readonly bool _showNonActive;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="showNonActive">Параметр "Показывать неактивные объяления".</param>
    public ActiveAdvertSpecification(bool showNonActive)
    {
        _showNonActive = showNonActive;
    }

    /// <inheritdoc/>
    public override Expression<Func<Advert, bool>> PredicateExpression =>
        advert => advert.Status == AdvertStatus.Published;
}
