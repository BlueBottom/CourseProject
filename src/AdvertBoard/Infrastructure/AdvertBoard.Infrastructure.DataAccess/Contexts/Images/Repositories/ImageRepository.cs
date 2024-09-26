using AdvertBoard.Application.AppServices.Contexts.Images.Repositories;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Domain.Contexts.Images;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Images.Repositories;

/// <inheritdoc/>
public class ImageRepository : IImageRepository
{
    private readonly IRepository<Image> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ImageRepository"/>.
    /// </summary>
    /// <param name="repository">Глупый репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    public ImageRepository(IRepository<Image> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<ImageDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var image = await _repository
            .GetAll()
            .Where(i => i.Id == id)
            .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        //TODO: исключение
        if (image is null) throw new Exception();
        return image;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(Image image, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(image, cancellationToken);
        return image.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var image = await _repository.GetByIdAsync(id, cancellationToken);
        // TODO: Сделать нормальное исключение.
        if (image is null) throw new Exception();
        await _repository.DeleteAsync(image, cancellationToken);
        return true;
    }
}