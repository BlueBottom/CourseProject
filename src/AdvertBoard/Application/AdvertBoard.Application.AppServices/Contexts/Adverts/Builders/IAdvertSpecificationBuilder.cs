using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;

/// <summary>
/// Создает спецификацию.
/// </summary>
public interface IAdvertSpecificationBuilder
{
    /// <summary>
    /// Строит спецификацию по запросу.
    /// </summary>
    /// <param name="getAllAdvertsDto"></param>
    /// <returns>Спецификация.</returns>
    Task<ISpecification<Advert>> Build(GetAllAdvertsDto getAllAdvertsDto);
}