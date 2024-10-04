using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Application.AppServices.Contexts.Images.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Application.AppServices.Services;
using AdvertBoard.Contracts.Contexts.Adverts.Responses;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Contracts.Contexts.Images.Requests;
using AdvertBoard.Contracts.Contexts.Images.Responses;
using AdvertBoard.Domain.Contexts.Images;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Images.Services;

/// <inheritdoc/>
public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IAdvertService _advertService;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BusinessLogicAbstractValidator<CreateImageRequest> _createImageValidator;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ImageService"/>.
    /// </summary>
    /// <param name="imageRepository">Репозиторий изображений.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="authorizationService">Сервис для реализации requirements.</param>
    /// <param name="httpContextAccessor">Передает HttpContext.</param>
    /// <param name="advertService">Сервис для работы с объявлениями.</param>
    /// <param name="createImageValidator">Валидатор запроса на добавление изображения.</param>
    public ImageService(
        IImageRepository imageRepository, 
        IMapper mapper,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor, 
        IAdvertService advertService, 
        BusinessLogicAbstractValidator<CreateImageRequest> createImageValidator
        )
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
        _advertService = advertService;
        _createImageValidator = createImageValidator;
    }

    /// <inheritdoc/>
    public Task<ImageResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _imageRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(CreateImageRequest createImageRequest, CancellationToken cancellationToken)
    {
        await _createImageValidator.ValidateAndThrowAsync(createImageRequest, cancellationToken);
        
        await EnsureResourceAuthorize(createImageRequest.AdvertId, cancellationToken);
        var image = _mapper.Map<CreateImageRequest, Image>(createImageRequest);
        
        return await _imageRepository.AddAsync(image, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingImage = await _imageRepository.GetByIdAsync(id, cancellationToken);
        await EnsureResourceAuthorize(existingImage.AdvertId, cancellationToken); 
        
        return await _imageRepository.DeleteAsync(id, cancellationToken);
    }
    
    private async Task EnsureResourceAuthorize(Guid advertId, CancellationToken cancellationToken)
    {
        var existingAdvert = await _advertService.GetByIdAsync(advertId, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingAdvert,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
    }
}