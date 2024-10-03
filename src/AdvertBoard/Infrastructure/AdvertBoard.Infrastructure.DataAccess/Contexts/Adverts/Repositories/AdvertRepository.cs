using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Contracts.Shared;
using AdvertBoard.Domain.Contexts.Adverts;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Adverts.Repositories;

/// <inheritdoc/>
public class AdvertRepository : IAdvertRepository
{
    private readonly IRepository<Advert> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализириует экземпляр класса <see cref="AdvertRepository"/>.
    /// </summary>
    /// <param name="repository">Глупый репозиторий</param>.
    /// <param name="mapper">Маппер.</param>
    public AdvertRepository(IRepository<Advert> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortAdvertDto>> GetByFilterWithPaginationAsync(PaginationRequest paginationRequest,
        ISpecification<Advert> specification,
        CancellationToken cancellationToken)
    {
        var result = new PageResponse<ShortAdvertDto>();
        
        var query = _repository.GetAll();
        
        var elementsCount = await query.CountAsync(cancellationToken);
        result.TotalPages = result.TotalPages = (int)Math.Ceiling((double)elementsCount / paginationRequest.BatchSize);

        var paginationQuery = await query
            .Where(specification.PredicateExpression)
            .OrderBy(advert => advert.Id)
            .Skip(paginationRequest.BatchSize * (paginationRequest.PageNumber - 1))
            .Take(paginationRequest.BatchSize)
            .ProjectTo<ShortAdvertDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
        
        result.Response = paginationQuery;
        return result;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(Advert advert, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(advert, cancellationToken);
        return advert.Id;
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, Advert updatedAdvert, CancellationToken cancellationToken)
    {
        var advert = await _repository.GetByIdAsync(id, cancellationToken);
        if (advert is null) throw new EntityNotFoundException("Объявление не было найдено.");
        _mapper.Map(updatedAdvert, advert);
        await _repository.UpdateAsync(advert, cancellationToken);
        return advert.Id;
    }

    /// <inheritdoc/>
    public async Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _repository.GetByIdAsync(id, cancellationToken);
        if (dto is null) throw new EntityNotFoundException("Объявление не было найдено.");
        return _mapper.Map<Advert, AdvertDto>(dto);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var advert = await _repository.GetByIdAsync(id, cancellationToken);
        if (advert is null) throw new EntityNotFoundException("Объявление не было найдено.");
        await _repository.DeleteAsync(advert, cancellationToken);
        return true;
    }
}