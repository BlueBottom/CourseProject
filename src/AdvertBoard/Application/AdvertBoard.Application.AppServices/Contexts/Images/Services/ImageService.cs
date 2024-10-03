using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Adverts.Services;
using AdvertBoard.Application.AppServices.Contexts.Images.Repositories;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Domain.Contexts.Images;
using AutoMapper;
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

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ImageService"/>.
    /// </summary>
    /// <param name="imageRepository">Репозиторий изображений.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="authorizationService">Сервис для реализации requirements.</param>
    /// <param name="httpContextAccessor">Передает HttpContext.</param>
    /// <param name="advertService">Сервис для работы с объявлениями.</param>
    public ImageService(
        IImageRepository imageRepository, 
        IMapper mapper,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor, 
        IAdvertService advertService)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
        _advertService = advertService;
    }

    /// <inheritdoc/>
    public Task<ImageDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _imageRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(CreateImageDto createImageDto, CancellationToken cancellationToken)
    {
        var existingAdvert = await _advertService.GetByIdAsync(createImageDto.AdvertId, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingAdvert,
            new ResourceOwnerRequirement());
        //TODO: исключение
        if (!authResult.Succeeded) throw new Exception("нет доступа");
        var image = _mapper.Map<CreateImageDto, Image>(createImageDto);
        return await _imageRepository.AddAsync(image, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingImage = await _imageRepository.GetByIdAsync(id, cancellationToken);
        var existingAdvert = await _advertService.GetByIdAsync(existingImage.AdvertId, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingAdvert,
            new ResourceOwnerRequirement());
        //TODO: исключение
        if (!authResult.Succeeded) throw new Exception("нет доступа");
        return await _imageRepository.DeleteAsync(id, cancellationToken);
    }
}