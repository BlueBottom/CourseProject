using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Builders;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using AdvertBoard.Contracts.Contexts.Adverts.Responses;
using AdvertBoard.Domain.Contexts.Adverts;
using AutoMapper;
using FluentValidation;
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
    private readonly BusinessLogicAbstractValidator<CreateAdvertRequest> _createAdvertValidator;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="advertRepository">Репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="advertSpecificationBuilder">Спецификация.</param>
    /// <param name="httpContextAccessor">Разрешает доступ к <see cref="HttpContext"/>.</param>
    /// <param name="authorizationService">Сервис для реализации requirements.</param>
    /// <param name="createAdvertValidator">Валидатор.</param>
    public AdvertService(
        IAdvertRepository advertRepository, 
        IMapper mapper,
        IAdvertSpecificationBuilder advertSpecificationBuilder, 
        IHttpContextAccessor httpContextAccessor,
        IAuthorizationService authorizationService, 
        BusinessLogicAbstractValidator<CreateAdvertRequest> createAdvertValidator)
    {
        _advertRepository = advertRepository;
        _mapper = mapper;
        _advertSpecificationBuilder = advertSpecificationBuilder;
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
        _createAdvertValidator = createAdvertValidator;
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortAdvertResponse>> GetByFilterWithPaginationAsync(GetAdvertsByFilterRequest getAdvertsByFilterRequest,
        CancellationToken cancellationToken)
    {
        var specification = await _advertSpecificationBuilder.Build(getAdvertsByFilterRequest);
        
        var paginationRequest = new PaginationRequest
        {
            BatchSize = getAdvertsByFilterRequest.BatchSize,
            PageNumber = getAdvertsByFilterRequest.PageNumber
        };

        return await _advertRepository.GetByFilterWithPaginationAsync(paginationRequest, specification, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(CreateAdvertRequest createAdvertRequest, CancellationToken cancellationToken)
    {
        await _createAdvertValidator.ValidateAndThrowAsync(createAdvertRequest, cancellationToken);
        
        var advert = _mapper.Map<CreateAdvertRequest, Advert>(createAdvertRequest);

        advert.UserId = _httpContextAccessor.GetAuthorizedUserId();
        
        return await _advertRepository.AddAsync(advert, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateAdvertRequest updateAdvertRequest, CancellationToken cancellationToken)
    {
        await EnsureResourceAuthorize(id, cancellationToken);
        
        return await _advertRepository.UpdateAsync(id, updateAdvertRequest, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<AdvertResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _advertRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await EnsureResourceAuthorize(id, cancellationToken);

        return await _advertRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<bool> ArchiveAsync(Guid id, CancellationToken cancellationToken)
    {
        await EnsureResourceAuthorize(id, cancellationToken);

        return await _advertRepository.ArchiveAsync(id, cancellationToken);
    }

    public async Task<bool> UnarchiveAsync(Guid id, CancellationToken cancellationToken)
    {
        await EnsureResourceAuthorize(id, cancellationToken);
        
        return await _advertRepository.UnarchiveAsync(id, cancellationToken);
    }

    private async Task EnsureResourceAuthorize(Guid resourceId, CancellationToken cancellationToken)
    {
        var existingAdvert = await _advertRepository.GetByIdAsync(resourceId, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(
            _httpContextAccessor.HttpContext.User, 
            existingAdvert,
            new ResourceOwnerRequirement()
            );
        
        if (!authResult.Succeeded) throw new ForbiddenException();
    }
}