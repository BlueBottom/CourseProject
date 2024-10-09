using System.Security.Claims;
using AdvertBoard.Application.AppServices.Exceptions;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Helpers;

/// <summary>
/// Класс со вспомогательными методами для HttpContextAccessor.
/// </summary>
public static class HttpContextHelper
{
    /// <summary>
    /// Получает идентификатор пользователя из клаимов.
    /// </summary>
    /// <param name="httpContextAccessor"><see cref="IHttpContextAccessor"/>.</param>
    /// <returns>Идентификатор пользователя.</returns>
    /// <exception cref="ForbiddenException">Исключение "Нет прав доступа".</exception>
    public static Guid GetAuthorizedUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var claims = httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new ForbiddenException();

        return Guid.Parse(claimId);
    }
}