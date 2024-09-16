using AdvertBoard.Contracts.Contexts.Adverts;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Services;

public interface IAdvertService
{
    Task<IEnumerable<ShortAdvertDto>> GetAllAsync(GetAllAdvertsDto dto, CancellationToken cancellationToken);
    Task<Guid> AddAsync(CreateAdvertDto dto, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(UpdateAdvertDto dto, CancellationToken cancellationToken);
    Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

}