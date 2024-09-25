using System.Security.Claims;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Adverts;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Services;

/// <inheritdoc/>
public class AdvertService : IAdvertService
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IMapper _mapper;
    private readonly IAdvertSpecificationBuilder _advertSpecificationBuilder;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="advertRepository">Репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="advertSpecificationBuilder">Спецификация.</param>
    public AdvertService(IAdvertRepository advertRepository, IMapper mapper, IAdvertSpecificationBuilder advertSpecificationBuilder, IHttpContextAccessor httpContextAccessor)
    {
        _advertRepository = advertRepository;
        _mapper = mapper;
        _advertSpecificationBuilder = advertSpecificationBuilder;
        _httpContextAccessor = httpContextAccessor;
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
        var advert = _mapper.Map<CreateAdvertDto, Advert>(createAdvertDto);
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrWhiteSpace(claimId))
        {
            return null;
        }

        var userId = Guid.Parse(claimId);

        advert.UserId = userId; 
        return _advertRepository.AddAsync(advert, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<Guid> UpdateAsync(Guid id, UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken)
    {
        var advert = _mapper.Map<UpdateAdvertDto, Advert>(updateAdvertDto);
        
        return _advertRepository.UpdateAsync(id, advert, cancellationToken);
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