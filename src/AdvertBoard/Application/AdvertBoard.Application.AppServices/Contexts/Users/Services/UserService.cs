using System.Security.Claims;
using AdvertBoard.Application.AppServices.Contexts.Users.Builders;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Shared;
using AdvertBoard.Domain.Contexts.Images;
using AdvertBoard.Domain.Contexts.Users;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Services;

/// <inheritdoc/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSpecificationBuilder _specificationBuilder;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="userRepository">Репозиторий.</param>
    /// <param name="specificationBuilder">Строитель спецификаций.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="httpContextAccessor">Http контекст.</param>
    public UserService(IUserRepository userRepository, IUserSpecificationBuilder specificationBuilder, IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _specificationBuilder = specificationBuilder;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Guid> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(claimId);
        var user = _mapper.Map<UpdateUserDto, User>(updateUserDto);
        user.Id = userId;
        return await _userRepository.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(CancellationToken cancellationToken)
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(claimId);
        return _userRepository.DeleteAsync(userId, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<PageResponse<ShortUserDto>> GetAllAsync(GetAllUsersDto getAllUsersDto,
        CancellationToken cancellationToken)
    {
        var specification = _specificationBuilder.Build(getAllUsersDto);

        var paginationRequest = new PaginationRequest
        {
            BatchSize = getAllUsersDto.BatchSize,
            PageNumber = getAllUsersDto.PageNumber
        };
        
        return _userRepository.GetAllAsync(specification, paginationRequest, cancellationToken);
    }
}