using AdvertBoard.Application.AppServices.Contexts.Adverts.Specifications;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;

/// <inheritdoc/>
public class AdvertSpecificationBuilder : IAdvertSpecificationBuilder
{
    /// <inheritdoc/>
    public ISpecification<Advert> Build(GetAllAdvertsDto dto)
    {
        ISpecification<Advert> specification = Specification<Advert>.True;

        if (!dto.ShowNonActive)
        {
            specification = specification.And(new ActiveAdvertSpecification(dto.ShowNonActive));
        }

        if (dto.CategoryId.HasValue)
        {
            specification = specification.And(new CategoryAdvertSpecification(dto.CategoryId.Value));
        }

        if (dto.Location.HasValue)
        {
            specification = specification.And(new LocationSpecification(dto.Location.Value));
        }

        if (dto.MaxPrice.HasValue)
        {
            specification = specification.And(new MaxPriceSpecification(dto.MaxPrice.Value));
        }

        if (dto.MinPrice.HasValue)
        {
            specification = specification.And(new MinPriceSpecification(dto.MinPrice.Value));
        }

        if (!string.IsNullOrWhiteSpace(dto.SearchString))
        {
            specification = specification.And(new SearchStringSpecification(dto.SearchString));
        }

        return specification;
    }
}