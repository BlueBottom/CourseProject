using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using AdvertBoard.Contracts.Contexts.Reviews.Responses;
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
    public async Task<PageResponse<ShortReviewResponse>> GetAllByUserIdAsync(GetAllReviewsRequest getAllReviewsRequest,
        CancellationToken cancellationToken)
    {
        var result = new PageResponse<ShortReviewResponse>();

        var query = _repository.GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.TotalPages =
            result.TotalPages = (int)Math.Ceiling((double)elementsCount / getAllReviewsRequest.BatchSize);

        var paginationQuery = await query
            .OrderBy(x => x.CreatedAt)
            .Where(x => x.ReceiverUserId == getAllReviewsRequest.UserId)
            .Skip(getAllReviewsRequest.BatchSize * (getAllReviewsRequest.PageNumber - 1))
            .Take(getAllReviewsRequest.BatchSize)
            .ProjectTo<ShortReviewResponse>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Response = paginationQuery;
        return result;
    }

    /// <inheritdoc/>
    public async Task<ReviewResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(id, cancellationToken);
        if (review is null) throw new EntityNotFoundException("Отзыв не был найден.");

        return _mapper.Map<Review, ReviewResponse>(review);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(Review review, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(review, cancellationToken);
        return review.Id;
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateReviewRequest updatedReviewRequest,
        CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(id, cancellationToken);
        if (review is null) throw new EntityNotFoundException("Отзыв не был найден.");

        _mapper.Map<UpdateReviewRequest, Review>(updatedReviewRequest, review);
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
    public Task<int[]> GetAllRatesByUser(Guid receiverUserId, CancellationToken cancellationToken)
    {
        var userRatingQuery = _repository
            .GetAll()
            .Where(x => x.ReceiverUserId == receiverUserId)
            .Select(x => x.Rating)
            .ToArrayAsync(cancellationToken);

        return userRatingQuery;
    }

    /// <inheritdoc/>
    public Task<bool> IsUserAlreadyLeftReview(Guid ownerUserId, Guid receiverUserId,
        CancellationToken cancellationToken)
    {
        return _repository
            .GetAll()
            .AnyAsync(x => x.OwnerUserId == ownerUserId && x.ReceiverUserId == receiverUserId, cancellationToken);
    }
}