using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Notifications.Services;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using AdvertBoard.Contracts.Contexts.Reviews.Responses;
using AdvertBoard.Domain.Contexts.Reviews;
using AutoMapper;
using FluentValidation;
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
    private readonly BusinessLogicAbstractValidator<CreateReviewRequest> _createReviewValidator;
    private readonly BusinessLogicAbstractValidator<GetAllReviewsRequest> _getAllReviewsValidator;
    private readonly INotificationService _notificationService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ReviewService"/>.
    /// </summary>
    public ReviewService(
        IReviewRepository reviewRepository, 
        IMapper mapper, 
        IHttpContextAccessor httpContextAccessor,
        IAuthorizationService authorizationService, 
        BusinessLogicAbstractValidator<CreateReviewRequest> createReviewValidator, 
        BusinessLogicAbstractValidator<GetAllReviewsRequest> getAllReviewsValidator, 
        INotificationService notificationService)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
        _createReviewValidator = createReviewValidator;
        _getAllReviewsValidator = getAllReviewsValidator;
        _notificationService = notificationService;
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortReviewResponse>> GetAllByUserIdAsync(GetAllReviewsRequest getAllReviewsRequest,
        CancellationToken cancellationToken)
    {
        await _getAllReviewsValidator.ValidateAndThrowAsync(getAllReviewsRequest, cancellationToken);
        
        return await _reviewRepository.GetAllByUserIdAsync(getAllReviewsRequest, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<ReviewResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _reviewRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(CreateReviewRequest createReviewRequest, CancellationToken cancellationToken)
    {
        await _createReviewValidator.ValidateAndThrowAsync(createReviewRequest, cancellationToken);
        
        var userId = _httpContextAccessor.GetAuthorizedUserId();
        
        var review = _mapper.Map<CreateReviewRequest, Review>(createReviewRequest);
        review.OwnerUserId = userId;

        var result= await _reviewRepository.AddAsync(review, cancellationToken);
        await _notificationService.SendReviewCreated(review.Id, review.ReceiverUserId, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateReviewRequest updateReviewRequest, CancellationToken cancellationToken)
    {
        var existingReview = await _reviewRepository.GetByIdAsync(id, cancellationToken);
        
        await EnsureResourceAuthorize(existingReview);

        var result = await _reviewRepository.UpdateAsync(id, updateReviewRequest, cancellationToken);
        await _notificationService.SendReviewCreated(existingReview.Id, existingReview.ReceiverUserId,
            cancellationToken);
        
        return result;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingReview = await _reviewRepository.GetByIdAsync(id, cancellationToken);
        
        await EnsureResourceAuthorize(existingReview);
        
        var result = await _reviewRepository.DeleteAsync(id, cancellationToken);
        await _notificationService.SendReviewCreated(existingReview.Id, existingReview.ReceiverUserId,
            cancellationToken);
        
        return result;
    }
    
    private async Task EnsureResourceAuthorize(ReviewResponse existingReview)
    {
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingReview,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
    }
}