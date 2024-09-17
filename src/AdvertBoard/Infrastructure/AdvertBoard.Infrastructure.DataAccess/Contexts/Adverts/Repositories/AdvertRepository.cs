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

    public async Task<IEnumerable<ShortAdvertDto>> GetAllAsync(ISpecification<Advert> specification,
        CancellationToken cancellationToken)
    {
        return await _repository
            .GetAll()
            .Where(specification.PredicateExpression)
            .ProjectTo<ShortAdvertDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(CreateAdvertDto createAdvertDto, Guid userId, CancellationToken cancellationToken)
    {
        var advert = _mapper.Map<CreateAdvertDto, Advert>(createAdvertDto);
        //TODO: перенести userid 
        advert.UserId = userId;
        await _repository.AddAsync(advert, cancellationToken);
        return advert.Id;
    }

    public async Task<Guid> UpdateAsync(Guid id, UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken)
    {
        var advert = await _repository.GetByIdAsync(id, cancellationToken);
        //TODO: Добавить нормальное исключение
        if (advert is null) throw new Exception();
        _mapper.Map(updateAdvertDto, advert);
        await _repository.UpdateAsync(advert, cancellationToken);
        return advert.Id;
    }

    public async Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _repository.GetByIdAsync(id, cancellationToken);
        //TODO: Добавить нормальное исключение
        if (dto is null) throw new Exception();
        return _mapper.Map<Advert, AdvertDto>(dto);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var advert = await _repository.GetByIdAsync(id, cancellationToken);
        //TODO: Добавить нормальное исключение
        if (advert is null) throw new Exception();
        await _repository.DeleteAsync(advert, cancellationToken);
        return true;
    }
}