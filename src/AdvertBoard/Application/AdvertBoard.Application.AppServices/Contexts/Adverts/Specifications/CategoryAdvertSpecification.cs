using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

/// <summary>
/// Спецификация поиска объявлений по категориям.
/// </summary>
public class CategoryAdvertSpecification : Specification<Advert>
{
    private readonly IEnumerable<Guid> _categoryIds;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="categoryIds">Идентификатор категории.</param>
    public CategoryAdvertSpecification(IEnumerable<Guid> categoryIds)
    {
        _categoryIds = categoryIds;
    }

    /// <inheritdoc/>
    public override Expression<Func<Advert, bool>> PredicateExpression => advert => 
        _categoryIds.Contains(advert.CategoryId);
}