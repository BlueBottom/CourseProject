using AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;
using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using AdvertBoard.Contracts.Enums;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;

/// <inheritdoc/>
public class AdvertSpecificationBuilder : IAdvertSpecificationBuilder
{
    private readonly ICategoryService _categoryService;

    public AdvertSpecificationBuilder(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <inheritdoc/>
    public async Task<ISpecification<Advert>> Build(GetAdvertsByFilterRequest getAdvertsByFilterRequest)
    {
        ISpecification<Advert> specification = Specification<Advert>.True;

        if (!getAdvertsByFilterRequest.ShowNonActive)
        {
            specification = specification.And(new ActiveAdvertSpecification());
        }
        else
        {
            specification = specification.And(Specification<Advert>.FromPredicate(x =>
                x.StatusId == AdvertStatus.Archived || x.StatusId == AdvertStatus.Published));
        }

        if (getAdvertsByFilterRequest.CategoryIds is not null && getAdvertsByFilterRequest.CategoryIds.Any())
        {
            var categoryIds =
                await _categoryService.GetHierarchyIdsAsync(getAdvertsByFilterRequest.CategoryIds,
                    CancellationToken.None);
            specification = specification.And(new CategoryAdvertSpecification(categoryIds));
        }

        if (getAdvertsByFilterRequest.Location.HasValue)
        {
            specification = specification.And(new LocationSpecification(getAdvertsByFilterRequest.Location.Value));
        }

        if (getAdvertsByFilterRequest.MaxPrice.HasValue)
        {
            specification = specification.And(new MaxPriceSpecification(getAdvertsByFilterRequest.MaxPrice.Value));
        }

        if (getAdvertsByFilterRequest.MinPrice.HasValue)
        {
            specification = specification.And(new MinPriceSpecification(getAdvertsByFilterRequest.MinPrice.Value));
        }

        if (!string.IsNullOrWhiteSpace(getAdvertsByFilterRequest.SearchString))
        {
            specification = specification.And(new SearchStringSpecification(getAdvertsByFilterRequest.SearchString));
        }

        return specification;
    }
}