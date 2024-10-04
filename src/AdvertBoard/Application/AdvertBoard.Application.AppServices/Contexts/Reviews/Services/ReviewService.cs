using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Reviews;
using AdvertBoard.Domain.Contexts.Reviews;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Reviews.Services;

/// <inheritdoc/>
public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserService _userService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ReviewService"/>.
    /// </summary>
    /// <param name="reviewRepository">Умный репозиторий для работы с отзывами.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="httpContextAccessor">Разрешает доступ к HttpContext.</param>
    /// <param name="authorizationService">Сервис для использования requirements.</param>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public ReviewService(IReviewRepository reviewRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor,
        IAuthorizationService authorizationService, IUserService userService)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
        _userService = userService;
    }

    /// <inheritdoc/>
    public Task<PageResponse<ShortReviewDto>> GetAllByUserIdAsync(GetAllReviewsDto getAllReviewsDto,
        CancellationToken cancellationToken)
    {
        return _reviewRepository.GetAllByUserIdAsync(getAllReviewsDto, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<ReviewDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _reviewRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(CreateReviewDto createReviewDto, CancellationToken cancellationToken)
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new ForbiddenException();
        
        var review = _mapper.Map<CreateReviewDto, Review>(createReviewDto);
        
        var userId = Guid.Parse(claimId);
        review.OwnerUserId = userId;

        var result= await _reviewRepository.AddAsync(review, cancellationToken);
        await UpdateRating(review.ReceiverUserId, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateReviewDto updateReviewDto, CancellationToken cancellationToken)
    {
        var existingReview = await _reviewRepository.GetByIdAsync(id, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingReview,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
        
        var result = await _reviewRepository.UpdateAsync(id, updateReviewDto, cancellationToken);
        await UpdateRating(existingReview.ReceiverUserId, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingReview = await _reviewRepository.GetByIdAsync(id, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingReview,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
        
        var result = await _reviewRepository.DeleteAsync(id, cancellationToken);
        await UpdateRating(existingReview.ReceiverUserId, cancellationToken);

        return result;
    }
    
    private async Task UpdateRating(Guid userId, CancellationToken cancellationToken)
    {
        var rating = await _reviewRepository.CalcUserRatingAsync(userId, cancellationToken);
        await _userService.UpdateRatingAsync(userId, rating, cancellationToken);
    }
}