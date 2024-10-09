using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Builders;
using AdvertBoard.Application.AppServices.Contexts.Users.Models;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Contracts.Contexts.Users.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Services;

/// <inheritdoc/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSpecificationBuilder _specificationBuilder;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;
    private readonly BusinessLogicAbstractValidator<UpdateUserRequest> _updateUserValidator;
    private readonly IReviewRepository _reviewRepository;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserService"/>.
    /// </summary>
    /// <param name="userRepository">Репозиторий.</param>
    /// <param name="specificationBuilder">Строитель спецификаций.</param>
    /// <param name="httpContextAccessor">Передает HttpContext.</param>
    /// <param name="authorizationService">Сервис для реализации requirements.</param>
    /// <param name="updateUserValidator">Валидатор обновления пользователя.</param>
    /// <param name="reviewRepository">Умный репозиторий для работы с отзывами.</param>
    public UserService(
        IUserRepository userRepository, 
        IUserSpecificationBuilder specificationBuilder, 
        IHttpContextAccessor httpContextAccessor, 
        IAuthorizationService authorizationService,
        BusinessLogicAbstractValidator<UpdateUserRequest> updateUserValidator, 
        IReviewRepository reviewRepository)
    {
        _userRepository = userRepository;
        _specificationBuilder = specificationBuilder;
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
        _updateUserValidator = updateUserValidator;
        _reviewRepository = reviewRepository;
    }

    /// <inheritdoc/>
    public Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(Guid id, UpdateUserRequest updateUserRequest,
        CancellationToken cancellationToken)
    {
        await EnsureResourceAuthorize(id, cancellationToken);

        await _updateUserValidator.ValidateAndThrowAsync(updateUserRequest, cancellationToken); 
        
        return await _userRepository.UpdateAsync(id, updateUserRequest, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await EnsureResourceAuthorize(id, cancellationToken);

        return await _userRepository.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<PageResponse<ShortUserResponse>> GetAllByFilterWithPaginationAsync(
        GetAllUsersByFilterRequest getAllUsersByFilterRequest,
        CancellationToken cancellationToken)
    {
        var specification = _specificationBuilder.Build(getAllUsersByFilterRequest);

        var paginationRequest = new PaginationRequest
        {
            BatchSize = getAllUsersByFilterRequest.BatchSize,
            PageNumber = getAllUsersByFilterRequest.PageNumber
        };

        return _userRepository.GetAllByFilterWithPaginationAsync(specification, paginationRequest, cancellationToken);
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

    private async Task EnsureResourceAuthorize(Guid id, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (existingUser is null) throw new EntityNotFoundException("Пользователь не был найден.");

        var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, existingUser,
            new ResourceOwnerRequirement());
        if (!authResult.Succeeded) throw new ForbiddenException();
    }
}