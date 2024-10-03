using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Contracts.Shared;
using AdvertBoard.Domain.Contexts.Adverts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Services;

/// <inheritdoc/>
public class AdvertService : IAdvertService
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IMapper _mapper;
    private readonly IAdvertSpecificationBuilder _advertSpecificationBuilder;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="advertRepository">Репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="advertSpecificationBuilder">Спецификация.</param>
    /// <param name="httpContextAccessor">Разрешает доступ к <see cref="HttpContext"/>.</param>
    /// <param name="authorizationService">Сервис для реализации requirements.</param>
    public AdvertService(IAdvertRepository advertRepository, IMapper mapper,
        IAdvertSpecificationBuilder advertSpecificationBuilder, IHttpContextAccessor httpContextAccessor,
        IAuthorizationService authorizationService)
    {
        _advertRepository = advertRepository;
        _mapper = mapper;
        _advertSpecificationBuilder = advertSpecificationBuilder;
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortAdvertDto>> GetByFilterWithPaginationAsync(GetAllAdvertsDto getAllAdvertsDto,
        CancellationToken cancellationToken)
    {
        var specification = await _advertSpecificationBuilder.Build(getAllAdvertsDto);
        
        var paginationRequest = new PaginationRequest
        {
            BatchSize = getAllAdvertsDto.BatchSize,
            PageNumber = getAllAdvertsDto.PageNumber
        };

        return await _advertRepository.GetByFilterWithPaginationAsync(paginationRequest, specification, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<Guid> AddAsync(CreateAdvertDto createAdvertDto, CancellationToken cancellationToken)
    {
        var advert = _mapper.Map<CreateAdvertDto, Advert>(createAdvertDto);
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new ForbiddenException();

        var userId = Guid.Parse(claimId);
        advert.UserId = userId;
        
        return _advertRepository.AddAsync(advert, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken)
    {
        var existingAdvert = await _advertRepository.GetByIdAsync(id, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, existingAdvert,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
        var advert = _mapper.Map<UpdateAdvertDto, Advert>(updateAdvertDto);

        return await _advertRepository.UpdateAsync(id, advert, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<AdvertDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _advertRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingAdvert = await _advertRepository.GetByIdAsync(id, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, existingAdvert,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
        
        return await _advertRepository.DeleteAsync(id, cancellationToken);
    }
}