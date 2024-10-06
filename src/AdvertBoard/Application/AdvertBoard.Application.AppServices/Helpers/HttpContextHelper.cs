using System.Security.Claims;
using AdvertBoard.Application.AppServices.Exceptions;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Helpers;

public static class HttpContextHelper
{
    public static Guid GetAuthorizedUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var claims = httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new ForbiddenException();

        return Guid.Parse(claimId);
    }
}