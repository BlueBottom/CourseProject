using System.Security.Claims;
using System.Text.Json;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Notifications.Services;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Domain.Contexts.Users;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Services;

/// <inheritdoc/>
public class AccountService : IAccountService
{
    private const string RedisPasswordRecoveryPrefix = "user_recovery_code:";

    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly BusinessLogicAbstractValidator<LoginUserRequest> _loginUserValidator;
    private readonly BusinessLogicAbstractValidator<RegisterUserRequest> _registerUserValidator;
    private readonly BusinessLogicAbstractValidator<AskRecoveryPasswordCodeRequest> _askRecoveryPasswordCodeValidator;
    private readonly INotificationService _notificationService;
    private readonly ILogger<AccountService> _logger;
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="AccountService"/>.
    /// </summary>
    public AccountService(
        IUserRepository userRepository,
        IConfiguration configuration,
        IMapper mapper,
        BusinessLogicAbstractValidator<LoginUserRequest> loginUserValidator,
        BusinessLogicAbstractValidator<RegisterUserRequest> registerUserValidator,
        INotificationService notificationService,
        ILogger<AccountService> logger,
        IDistributedCache distributedCache, 
        BusinessLogicAbstractValidator<AskRecoveryPasswordCodeRequest> askRecoveryPasswordCodeValidator)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
        _loginUserValidator = loginUserValidator;
        _registerUserValidator = registerUserValidator;
        _notificationService = notificationService;
        _logger = logger;
        _distributedCache = distributedCache;
        _askRecoveryPasswordCodeValidator = askRecoveryPasswordCodeValidator;
    }

    /// <inheritdoc/>
    public async Task<Guid> RegisterAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Поступил запрос {nameof(RegisterUserRequest)} : {{Request}}",
            JsonSerializer.Serialize(registerUserRequest));
        await _registerUserValidator.ValidateAndThrowAsync(registerUserRequest, cancellationToken);

        registerUserRequest.Phone = PhoneHelper.NormalizePhoneNumber(registerUserRequest.Phone!);
        var user = _mapper.Map<RegisterUserRequest, User>(registerUserRequest);
        var password = CryptoHelper.GetBase64Hash(registerUserRequest.Password!);

        var id = await _userRepository.AddAsync(user, password, cancellationToken);
        await _notificationService.SendUserRegistered(user.Name, user.Email, cancellationToken);

        return id;
    }

    /// <inheritdoc/>
    public async Task<string> LoginAsync(LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
    {
        await _loginUserValidator.ValidateAndThrowAsync(loginUserRequest, cancellationToken);

        var existingUser = await _userRepository.FindByEmail(loginUserRequest.Email!, cancellationToken);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, existingUser?.Id.ToString()!),
            new Claim(ClaimTypes.Name, existingUser?.Name!),
            new Claim(ClaimTypes.Role, existingUser?.RoleId.ToString()!)
        };

        var result = JwtHelper.GenerateJwtToken(claims, _configuration);

        return result;
    }

    /// <inheritdoc/>
    public async Task AskRecoveryPasswordCode(AskRecoveryPasswordCodeRequest request, CancellationToken cancellationToken)
    {
        await _askRecoveryPasswordCodeValidator.ValidateAndThrowAsync(request, cancellationToken);
        
        var code = CodeGeneratorHelper.GenerateOtp();

        await _distributedCache.PutByKeyAsync($"{RedisPasswordRecoveryPrefix}{request.Email}", code, 60, cancellationToken); ;
        await _notificationService.SendPasswordRecoveryCode(request.Email!, code, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task RecoverPasswordWithCode(RecoverPasswordWithCodeRequest request,
        CancellationToken cancellationToken)
    {
        var key = $"{RedisPasswordRecoveryPrefix}{request.Email}";
        var cachedCode = await _distributedCache.GetByKeyAsync<string>($"{RedisPasswordRecoveryPrefix}{request.Email}", cancellationToken);
        if (cachedCode == request.Code)
        {
            var password = CryptoHelper.GetBase64Hash(request.Password);
            await _userRepository.ChangePassword(request.Email, password, cancellationToken);
            await _distributedCache.RemoveAsync(key, cancellationToken);
            await _notificationService.SendPasswordSuccessfullyRecovered(request.Email, cancellationToken);
        }
        else throw new EntityNotFoundException("Код не валиден.");
    }
}