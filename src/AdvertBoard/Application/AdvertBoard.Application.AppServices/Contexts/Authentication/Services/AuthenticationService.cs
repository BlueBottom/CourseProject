using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Domain.Contexts.Users;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertBoard.Application.AppServices.Contexts.Authentication.Services;

/// <inheritdoc/>
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly BusinessLogicAbstractValidator<LoginUserRequest> _loginUserValidator;
    private readonly BusinessLogicAbstractValidator<RegisterUserRequest> _registerUserValidator;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="AuthenticationService"/>.
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="loginUserValidator">Валидатор логина.</param>
    /// <param name="registerUserValidator">Валидатор регистрации.</param>
    public AuthenticationService(
        IUserRepository userRepository, 
        IConfiguration configuration, 
        IMapper mapper,
        BusinessLogicAbstractValidator<LoginUserRequest> loginUserValidator, 
        BusinessLogicAbstractValidator<RegisterUserRequest> registerUserValidator)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
        _loginUserValidator = loginUserValidator;
        _registerUserValidator = registerUserValidator;
    }

    /// <inheritdoc/>
    public async Task<Guid> RegisterAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        await _registerUserValidator.ValidateAndThrowAsync(registerUserRequest, cancellationToken);

        registerUserRequest.Phone = PhoneHelper.NormalizePhoneNumber(registerUserRequest.Phone);
        var user = _mapper.Map<RegisterUserRequest, User>(registerUserRequest);
        var password = CryptoHelper.GetBase64Hash(registerUserRequest.Password);
        
        return await _userRepository.AddAsync(user, password, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<string> LoginAsync(LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
    {
        await _loginUserValidator.ValidateAndThrowAsync(loginUserRequest, cancellationToken);

        var existingUser = await _userRepository.FindByEmail(loginUserRequest.Email, cancellationToken);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, existingUser?.Id.ToString()!),
            new Claim(ClaimTypes.Name, existingUser?.Name!),
            new Claim(ClaimTypes.Role, existingUser?.RoleId.ToString()!)
        };

        var secretKey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var token = new JwtSecurityToken
        (
            claims: claims,
            issuer: issuer,
            audience: audience,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256
            )
        );

        var result = new JwtSecurityTokenHandler().WriteToken(token);

        return result;
    }
}