using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Comments;
using AdvertBoard.Domain.Contexts.Comments;
using AutoMapper;
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

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CommentService"/>.
    /// </summary>
    /// <param name="commentRepository">Умный репозиторий для работы с комментариями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="authorizationService">Сервис для авторизации по ресурсам.</param>
    /// <param name="httpContextAccessor">Разрешает доступ к HttpContext.</param>
    public CommentService(ICommentRepository commentRepository, IMapper mapper,
        IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public Task<Guid> AddAsync(CreateCommentDto createCommentDto, CancellationToken cancellationToken)
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new ForbiddenException();

        var userId = Guid.Parse(claimId);
        var comment = _mapper.Map<CreateCommentDto, Comment>(createCommentDto);
        comment.UserId = userId;

        return _commentRepository.AddAsync(comment, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<PageResponse<ShortCommentDto>> GetAllWithPaginationAsync(GetAllCommentsDto getAllCommentsDto,
        CancellationToken cancellationToken)
    {
        return _commentRepository.GetAllWithPaginationAsync(getAllCommentsDto, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<CommentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _commentRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateCommentDto updateCommentDto, CancellationToken cancellationToken)
    {
        var existingReview =  await _commentRepository.GetByIdAsync(id, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingReview,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();

        return await _commentRepository.UpdateAsync(id, updateCommentDto, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingReview =  await _commentRepository.GetByIdAsync(id, cancellationToken);
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,
            existingReview,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();

        return await _commentRepository.DeleteAsync(id, cancellationToken);
    }

    public Task<CommentHierarchyDto> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _commentRepository.GetHierarchyByIdAsync(id, cancellationToken);
    }
}