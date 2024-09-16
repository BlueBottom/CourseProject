using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Adverts;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Adverts.Repositories;

public class AdvertRepository : IAdvertRepository
{
    private readonly IRepository<Advert> _repository;
    private readonly IMapper _mapper;

    public AdvertRepository(IRepository<Advert> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShortAdvertDto>> GetAllAsync(ISpecification<Advert> specification, CancellationToken cancellationToken)
    {
        return await _repository
            .GetAll()
            .Where(specification.PredicateExpression)
            .ProjectTo<ShortAdvertDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(CreateAdvertDto dto, CancellationToken cancellationToken)
    {
        var advert = _mapper.Map<CreateAdvertDto, Advert>(dto);
        advert.UserId = new Guid("850aab5b-2ce3-4561-bf68-0166fa448d44");
        await _repository.AddAsync(advert, cancellationToken);
        return advert.Id;
    }

    public Task<Guid> UpdateAsync(UpdateAdvertDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}