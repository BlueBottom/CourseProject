using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Contracts.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AdvertBoard.Application.AppServices.Authorization.Handlers;

/// <summary>
/// Обработчик ограничителя доступа администратора.
/// </summary>
public class IsAdminHandler :  AuthorizationHandler<ResourceOwnerRequirement>
{
    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOwnerRequirement ownerRequirement)
    {
        var claimRole = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (claimRole is null) return Task.CompletedTask;
        if (claimRole == UserRole.Admin.ToString()) context.Succeed(ownerRequirement);
        return Task.CompletedTask;
    }
}