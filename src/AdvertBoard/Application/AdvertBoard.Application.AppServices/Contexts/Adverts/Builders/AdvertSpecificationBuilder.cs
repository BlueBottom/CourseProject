using AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;
using AdvertBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Adverts;
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
    public async Task<ISpecification<Advert>> Build(GetAllAdvertsDto getAllAdvertsDto)
    {
        ISpecification<Advert> specification = Specification<Advert>.True;

        if (!getAllAdvertsDto.ShowNonActive)
        {
            specification = specification.And(new ActiveAdvertSpecification(getAllAdvertsDto.ShowNonActive));
        }

        if (getAllAdvertsDto.CategoryIds is not null && getAllAdvertsDto.CategoryIds.Any())
        {
            var categoryIds = await _categoryService.GetHierarchyIdsAsync(getAllAdvertsDto.CategoryIds, CancellationToken.None);
            specification = specification.And(new CategoryAdvertSpecification(categoryIds));
        }

        if (getAllAdvertsDto.Location.HasValue)
        {
            specification = specification.And(new LocationSpecification(getAllAdvertsDto.Location.Value));
        }

        if (getAllAdvertsDto.MaxPrice.HasValue)
        {
            specification = specification.And(new MaxPriceSpecification(getAllAdvertsDto.MaxPrice.Value));
        }

        if (getAllAdvertsDto.MinPrice.HasValue)
        {
            specification = specification.And(new MinPriceSpecification(getAllAdvertsDto.MinPrice.Value));
        }

        if (!string.IsNullOrWhiteSpace(getAllAdvertsDto.SearchString))
        {
            specification = specification.And(new SearchStringSpecification(getAllAdvertsDto.SearchString));
        }

        return specification;
    }
}