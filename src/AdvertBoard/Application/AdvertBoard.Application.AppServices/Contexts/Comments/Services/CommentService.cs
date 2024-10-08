using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Comments.Requests;
using AdvertBoard.Contracts.Contexts.Comments.Responses;
using AdvertBoard.Domain.Contexts.Comments;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Comments.Services;

/// <inheritdoc/>
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BusinessLogicAbstractValidator<CreateCommentRequest> _createCommentValidator;
    private readonly BusinessLogicAbstractValidator<GetAllCommentsRequest> _getAllCommentsValidator;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CommentService"/>.
    /// </summary>
    /// <param name="commentRepository">Умный репозиторий для работы с комментариями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="authorizationService">Сервис для авторизации по ресурсам.</param>
    /// <param name="httpContextAccessor">Разрешает доступ к HttpContext.</param>
    /// <param name="createCommentValidator">Валидатор запроса на создание комментария.</param>
    /// <param name="getAllCommentsValidator">Валидатор запроса на получение комментариев к объявлению.</param>
    public CommentService(
        ICommentRepository commentRepository,
        IMapper mapper,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor,
        BusinessLogicAbstractValidator<CreateCommentRequest> createCommentValidator,
        BusinessLogicAbstractValidator<GetAllCommentsRequest> getAllCommentsValidator
        )
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
        _createCommentValidator = createCommentValidator;
        _getAllCommentsValidator = getAllCommentsValidator;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(CreateCommentRequest createCommentRequest, CancellationToken cancellationToken)
    {
        await _createCommentValidator.ValidateAndThrowAsync(createCommentRequest, cancellationToken);

        var comment = _mapper.Map<CreateCommentRequest, Comment>(createCommentRequest);
        comment.UserId = _httpContextAccessor.GetAuthorizedUserId();

        return await _commentRepository.AddAsync(comment, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortCommentResponse>> GetAllWithPaginationAsync(
        GetAllCommentsRequest getAllCommentsRequest,
        CancellationToken cancellationToken)
    {
        await _getAllCommentsValidator.ValidateAndThrowAsync(getAllCommentsRequest, cancellationToken);
        
        return await _commentRepository.GetAllWithPaginationAsync(getAllCommentsRequest, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<CommentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _commentRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateCommentRequest updateCommentRequest,
        CancellationToken cancellationToken)
    {
        await EnsureResourceAuthorize(id, cancellationToken);

        return await _commentRepository.UpdateAsync(id, updateCommentRequest, cancellationToken);
    }

    private async Task EnsureResourceAuthorize(Guid id, CancellationToken cancellationToken)
    {
        var existingReview = await _commentRepository.GetByIdAsync(id, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingReview,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingReview = await _commentRepository.GetByIdAsync(id, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingReview,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();

        return await _commentRepository.DeleteAsync(id, cancellationToken);
    }

    public Task<CommentHierarchyResponse> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _commentRepository.GetHierarchyByIdAsync(id, cancellationToken);
    }
}