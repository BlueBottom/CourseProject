using AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Adverts;
using AutoMapper;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Services;

public class AdvertService : IAdvertService
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IMapper _mapper;
    private readonly IAdvertSpecificationBuilder _advertSpecificationBuilder;

    public AdvertService(IAdvertRepository advertRepository, IMapper mapper, IAdvertSpecificationBuilder advertSpecificationBuilder)
    {
        _advertRepository = advertRepository;
        _mapper = mapper;
        _advertSpecificationBuilder = advertSpecificationBuilder;
    }

    public Task<IEnumerable<ShortAdvertDto>> GetAllAsync(GetAllAdvertsDto dto,
        CancellationToken cancellationToken)
    {   
        var specification = _advertSpecificationBuilder.Build(dto);

        return _advertRepository.GetAllAsync(specification, cancellationToken);
    }

    public Task<Guid> AddAsync(CreateAdvertDto dto, CancellationToken cancellationToken)
    {
        return  _advertRepository.AddAsync(dto, cancellationToken);
    }

    public Task<Guid> UpdateAsync(UpdateAdvertDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return  _advertRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}