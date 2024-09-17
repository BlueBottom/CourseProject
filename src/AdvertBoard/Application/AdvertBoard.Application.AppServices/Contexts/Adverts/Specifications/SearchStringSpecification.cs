using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

/// <summary>
/// Спецификация строки поиска.
/// </summary>
public class SearchStringSpecification : Specification<Advert>
{
    private readonly string _searchString;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="searchString">Строка поиска.</param>
    public SearchStringSpecification(string searchString)
    {
        _searchString = searchString;
    }

    /// <inheritdoc/>
    public override Expression<Func<Advert, bool>> PredicateExpression => advert =>
        advert.Title.ToLower().Contains(_searchString) ||
        advert.Description.ToLower().Contains(_searchString);
}
