using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Contexts.Reviews;
using AdvertBoard.Contracts.Shared;
using AdvertBoard.Domain.Contexts.Reviews;
using AdvertBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Reviews.Repositories;

/// <inheritdoc/>
public class ReviewRepository : IReviewRepository
{
    private readonly IRepository<Review> _repository;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ReviewRepository"/>.
    /// </summary>
    /// <param name="repository">Глупый репозиторий.</param>
    /// <param name="mapper">Маппер.</param>
    public ReviewRepository(IRepository<Review> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortReviewDto>> GetAllByUserIdAsync(GetAllReviewsDto getAllReviewsDto,
        CancellationToken cancellationToken)
    {
        var result = new PageResponse<ShortReviewDto>();
        
        var query = _repository.GetAll();
        
        var elementsCount = await query.CountAsync(cancellationToken);
        result.TotalPages = result.TotalPages = (int)Math.Ceiling((double)elementsCount / getAllReviewsDto.BatchSize);
        
        var paginationQuery = await query
            .OrderBy(x => x.CreatedAt)
            .Where(x => x.ReceiverUserId == getAllReviewsDto.UserId)
            .Skip(getAllReviewsDto.BatchSize * (getAllReviewsDto.PageNumber - 1))
            .Take(getAllReviewsDto.BatchSize)
            .ProjectTo<ShortReviewDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Response = paginationQuery;
        return result;
    }

    /// <inheritdoc/>
    public async Task<ReviewDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var review =  await _repository.GetByIdAsync(id, cancellationToken);
        if (review is null) throw new EntityNotFoundException("Отзыв не был найден.");
        
        return _mapper.Map<Review, ReviewDto>(review);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(Review review, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(review, cancellationToken);
        return review.Id;
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateReviewDto updatedReviewDto, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(id, cancellationToken);
        if (review is null) throw new EntityNotFoundException("Отзыв не был найден.");
        
        _mapper.Map<UpdateReviewDto, Review>(updatedReviewDto, review);
        await _repository.UpdateAsync(review, cancellationToken);
        return review.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(id, cancellationToken);
        if (review is null) throw new EntityNotFoundException("Отзыв не был найден.");
        await _repository.DeleteAsync(review, cancellationToken);
        
        return true;

    }

    /// <inheritdoc/>
    public async Task<decimal?> CalcUserRatingAsync(Guid id, CancellationToken cancellationToken)
    {
        var userRatingQuery = _repository
            .GetAll()
            .Where(x => x.ReceiverUserId == id)
            .Select(x => x.Rating);

        if (!await userRatingQuery.AnyAsync(cancellationToken)) return null;

        var userRating = (decimal) await userRatingQuery.AverageAsync(cancellationToken);
        return userRating;
    }
}