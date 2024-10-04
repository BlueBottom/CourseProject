using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Users.Builders;
using AdvertBoard.Application.AppServices.Contexts.Users.Models;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Contracts.Contexts.Users.Responses;
using AdvertBoard.Domain.Contexts.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Services;

/// <inheritdoc/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSpecificationBuilder _specificationBuilder;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserService"/>.
    /// </summary>
    /// <param name="userRepository">Репозиторий.</param>
    /// <param name="specificationBuilder">Строитель спецификаций.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="httpContextAccessor">Передает HttpContext.</param>
    /// <param name="authorizationService">Сервис для реализации requirements.</param>
    public UserService(IUserRepository userRepository, IUserSpecificationBuilder specificationBuilder, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _specificationBuilder = specificationBuilder;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
    }

    /// <inheritdoc/>
    public Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid userId, UpdateUserRequest updateUserRequest, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (existingUser is null) throw new EntityNotFoundException("Пользователь не был найден.");
        
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, existingUser,
                new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
        
        var user = _mapper.Map<UpdateUserRequest, User>(updateUserRequest);
        
        return await _userRepository.UpdateAsync(userId, user, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (existingUser is null) throw new EntityNotFoundException("Пользователь не был найден.");
        
        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, existingUser,
                new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
        
        return await _userRepository.DeleteAsync(userId, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<PageResponse<ShortUserResponse>> GetAllByFilterWithPaginationAsync(GetAllUsersRequest getAllUsersRequest,
        CancellationToken cancellationToken)
    {
        var specification = _specificationBuilder.Build(getAllUsersRequest);

        var paginationRequest = new PaginationRequest
        {
            BatchSize = getAllUsersRequest.BatchSize,
            PageNumber = getAllUsersRequest.PageNumber
        };
        
        return _userRepository.GetAllByFilterWithPaginationAsync(specification, paginationRequest, cancellationToken);
    }

    /// <inheritdoc/>
    public Task UpdateRatingAsync(Guid id, decimal? rating, CancellationToken cancellationToken)
    {
        return _userRepository.UpdateRatingAsync(id, rating, cancellationToken);
    }
    
    
    /// <inheritdoc/>
    public Task<UserWithPasswordModel?> FindByEmail(string email, CancellationToken cancellationToken)
    {
        return _userRepository.FindByEmail(email, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> IsExistByPhone(string phone, CancellationToken cancellationToken)
    {
        return _userRepository.IsExistByPhone(phone, cancellationToken);
    }
}