using AdvertBoard.Application.AppServices.Contexts.Images.Repositories;
using AdvertBoard.Contracts.Contexts.Images;
using AutoMapper;

namespace AdvertBoard.Application.AppServices.Contexts.Images.Services;

/// <inheritdoc/>
public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="imageRepository">Репозиторий изображений.</param>
    /// <param name="mapper">Маппер.</param>
    public ImageService(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public Task<ImageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _imageRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<Guid> AddAsync(CreateImageDto createImageDto, CancellationToken cancellationToken)
    {
        return _imageRepository.AddAsync(createImageDto, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _imageRepository.DeleteAsync(id, cancellationToken);
    }
}