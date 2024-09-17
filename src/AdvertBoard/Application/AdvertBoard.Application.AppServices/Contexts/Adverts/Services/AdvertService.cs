using AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Contracts.Contexts.Adverts;
using AutoMapper;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Services;

/// <inheritdoc/>
public class AdvertService : IAdvertService
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IMapper _mapper;
    private readonly IAdvertSpecificationBuilder _advertSpecificationBuilder;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="advertRepository">Репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="advertSpecificationBuilder">Спецификация.</param>
    public AdvertService(IAdvertRepository advertRepository, IMapper mapper, IAdvertSpecificationBuilder advertSpecificationBuilder)
    {
        _advertRepository = advertRepository;
        _mapper = mapper;
        _advertSpecificationBuilder = advertSpecificationBuilder;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ShortAdvertDto>> GetAllAsync(GetAllAdvertsDto getAllAdvertsDto,
        CancellationToken cancellationToken)
    {   
        var specification = _advertSpecificationBuilder.Build(getAllAdvertsDto);

        return _advertRepository.GetAllAsync(specification, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<Guid> AddAsync(CreateAdvertDto createAdvertDto, CancellationToken cancellationToken)
    {
        return _advertRepository.AddAsync(createAdvertDto, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<Guid> UpdateAsync(Guid id, UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken)
    {
        return _advertRepository.UpdateAsync(id, updateAdvertDto, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _advertRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _advertRepository.DeleteAsync(id, cancellationToken);
    }
}