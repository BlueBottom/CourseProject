using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Contracts.Contexts.Adverts.Responses;
using Microsoft.AspNetCore.Authorization;

namespace AdvertBoard.Application.AppServices.Authorization.Handlers;

/// <summary>
/// Обрабочик ограничителя доступа по владению ресурса <see cref="AdvertResponse"/>.
/// </summary>
public class IsAdvertOwnerHandler : AuthorizationHandler<ResourceOwnerRequirement, AdvertResponse>
{
    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerRequirement ownerRequirement, 
        AdvertResponse resource)
    {
        var claimId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (claimId is null) return Task.CompletedTask;
        var userId = Guid.Parse(claimId.Value);

        if (userId == resource.UserId) context.Succeed(ownerRequirement);

        return Task.CompletedTask;
    }
}