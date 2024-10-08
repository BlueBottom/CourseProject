using AdvertBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Comments;
using AdvertBoard.Contracts.Contexts.Comments.Requests;
using AdvertBoard.Contracts.Contexts.Comments.Responses;
using AdvertBoard.Domain.Contexts.Comments;
using AdvertBoard.Infrastructure.Repository.Relational;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Comments.Repositories;

/// <inheritdoc/>
public class CommentRepository : ICommentRepository
{
    private readonly IRelationalRepository<Comment> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CommentRepository"/>.
    /// </summary>
    /// <param name="repository">Глупый репозиторий, поддерживающий SQL скрипты.</param>
    /// <param name="mapper">Маппер.</param>
    public CommentRepository(IRelationalRepository<Comment> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(Comment comment, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(comment, cancellationToken);
        return comment.Id;
    }

    /// <inheritdoc/>
    public async Task<PageResponse<ShortCommentResponse>> GetAllWithPaginationAsync(GetAllCommentsRequest getAllCommentsRequest,
        CancellationToken cancellationToken)
    {
        var result = new PageResponse<ShortCommentResponse>();

        var query = _repository.GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.TotalPages = result.TotalPages = (int)Math.Ceiling((double)elementsCount / getAllCommentsRequest.BatchSize);
        
        var paginationQuery = await query
            .OrderBy(x => x.CreatedAt)
            .Where(x => x.AdvertId == getAllCommentsRequest.AdvertId)
            .Skip(getAllCommentsRequest.BatchSize * (getAllCommentsRequest.PageNumber - 1))
            .Take(getAllCommentsRequest.BatchSize)
            .ProjectTo<ShortCommentResponse>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Response = paginationQuery;
        return result;
    }

    /// <inheritdoc/>
    public async Task<CommentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(id, cancellationToken);
        if (comment is null) throw new EntityNotFoundException("Комментарий не был найден");

        return _mapper.Map<Comment, CommentResponse>(comment);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateCommentRequest updateCommentRequest, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(id, cancellationToken);
        if (comment is null) throw new EntityNotFoundException("Комментарий не был найден");
        _mapper.Map<UpdateCommentRequest, Comment>(updateCommentRequest, comment);
        await _repository.UpdateAsync(comment, cancellationToken);

        return comment.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(id, cancellationToken);
        if (comment is null) throw new EntityNotFoundException("Комментарий не был найден");
        await _repository.DeleteAsync(comment, cancellationToken);

        return true;
    }

    /// <inheritdoc/>
    public async Task<CommentHierarchyResponse> GetHierarchyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        string sql =
            """
            WITH RECURSIVE r AS (
                SELECT c."Id", c."Content", c."ParentId", c."CreatedAt", c."AdvertId", c."UserId", c."EditedAt", c."IsActive"
                FROM public."Comment" c
                WHERE "Id" = {0}
                UNION
                SELECT c."Id", c."Content", c."ParentId", c."CreatedAt", c."AdvertId", c."UserId", c."EditedAt", c."IsActive"
                FROM public."Comment" c
                JOIN r ON c."ParentId" = r."Id"
            )
            SELECT * FROM r
            """;
        var query = await _repository
            .GetBySql(sql, id)
            .AsNoTrackingWithIdentityResolution()
            .ToArrayAsync(cancellationToken);
        return _mapper.Map<CommentHierarchyResponse>(query.FirstOrDefault());
    }

    /// <inheritdoc/>
    public Task<bool> IsCommentExists(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll().AnyAsync(x => x.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> IsCurrentCommentRelatedToCurrentAdvert(Guid? parentId, Guid? advertId, CancellationToken cancellationToken)
    {
        return _repository.GetAll().AnyAsync(x => x.Id == parentId && x.AdvertId == advertId, cancellationToken);
    }
}