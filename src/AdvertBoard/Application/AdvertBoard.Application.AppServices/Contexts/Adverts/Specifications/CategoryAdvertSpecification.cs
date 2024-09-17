using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

/// <summary>
/// Спецификация категории.
/// </summary>
public class CategoryAdvertSpecification : Specification<Advert>
{
    private readonly Guid _categoryId;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    public CategoryAdvertSpecification(Guid categoryId)
    {
        _categoryId = categoryId;
    }

    /// <inheritdoc/>
    public override Expression<Func<Advert, bool>> PredicateExpression => advert => advert.CategoryId == _categoryId;
}