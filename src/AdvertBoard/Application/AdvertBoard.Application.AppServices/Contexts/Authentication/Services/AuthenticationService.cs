using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Domain.Contexts.Users;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertBoard.Application.AppServices.Contexts.Authentication.Services;

/// <inheritdoc/>
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
    {
        //TODO: Нормальное исключение.
        if (await _userRepository.FindUser(x => x.Email == registerUserDto.Email, cancellationToken) is not null)
            throw new Exception();
        if (await _userRepository.FindUser(x => x.Phone == registerUserDto.Phone, cancellationToken) is not null)
            throw new Exception();

        var user = _mapper.Map<RegisterUserDto, User>(registerUserDto);
        var password = CryptoHelper.GetBase64Hash(registerUserDto.Password);
        return await _userRepository.AddUser(user, password, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<string> LoginAsync(LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FindUser(x => x.Email == loginUserDto.Email, cancellationToken);
        // TODO: исключение.
        if (existingUser is null) throw new Exception();

        var password = CryptoHelper.GetBase64Hash(loginUserDto.Password);
        //TODO: исключение.
        if (!existingUser.Password.Equals(password)) throw new Exception();
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
            new Claim(ClaimTypes.Name, existingUser.Name)
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