using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;

public interface IAdvertRepository
{
    Task<IEnumerable<ShortAdvertDto>> GetAllAsync(ISpecification<Advert> specification,
        CancellationToken cancellationToken);
    Task<Guid> AddAsync(CreateAdvertDto dto, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(UpdateAdvertDto dto, CancellationToken cancellationToken);
    Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}