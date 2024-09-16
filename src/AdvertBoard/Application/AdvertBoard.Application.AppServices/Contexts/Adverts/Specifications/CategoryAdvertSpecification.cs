using System.Linq.Expressions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;

public class CategoryAdvertSpecification : Specification<Advert>
{
    private readonly Guid _categoryId;

    public CategoryAdvertSpecification(Guid categoryId)
    {
        _categoryId = categoryId;
    }

    public override Expression<Func<Advert, bool>> PredicateExpression => advert => advert.CategoryId == _categoryId;
}