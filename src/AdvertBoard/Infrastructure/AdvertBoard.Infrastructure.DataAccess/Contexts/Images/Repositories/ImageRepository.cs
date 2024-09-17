using AdvertBoard.Application.AppServices.Contexts.Images.Repositories;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Domain.Contexts.Images;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Images.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly IRepository<Image> _repository;
    private readonly IMapper _mapper;

    public ImageRepository(IRepository<Image> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<ImageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _repository
            .GetAll()
            .Where(i => i.Id == id)
            .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(CreateImageDto createImageDto, CancellationToken cancellationToken)
    {
        var image = _mapper.Map<CreateImageDto, Image>(createImageDto);
        await _repository.AddAsync(image, cancellationToken);
        return image.Id;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var image = await _repository.GetByIdAsync(id, cancellationToken);
        // TODO: Сделать нормальное исключение.
        if (image is null) throw new Exception();
        await _repository.DeleteAsync(image, cancellationToken);
        return true;
    }
}