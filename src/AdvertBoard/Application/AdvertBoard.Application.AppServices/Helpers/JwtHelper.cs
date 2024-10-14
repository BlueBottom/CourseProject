using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertBoard.Application.AppServices.Helpers;

/// <summary>
/// Вспомогательный класс для работы с JWT.
/// </summary>
public class JwtHelper
{
    /// <summary>
    /// Генерирует JWT токен.
    /// </summary>
    /// <param name="claims">Список клаимов.</param>
    /// <param name="configuration">Конфигурация сборки.</param>
    /// <returns>Jwt-токен.</returns>
    public static string GenerateJwtToken(List<Claim> claims, IConfiguration configuration)
    {
        var secretKey = configuration["Jwt:Key"];
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];

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