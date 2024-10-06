using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Contracts.Contexts.Reviews;
using AdvertBoard.Contracts.Contexts.Reviews.Responses;
using Microsoft.AspNetCore.Authorization;

namespace AdvertBoard.Application.AppServices.Authorization.Handlers;

/// <summary>
/// Обрабочик ограничителя доступа по владению ресурса <see cref="ReviewResponse"/>.
/// </summary>
public class IsReviewOwnerHandler : AuthorizationHandler<ResourceOwnerRequirement, ReviewResponse>
{
    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerRequirement ownerRequirement, 
        ReviewResponse resource)
    {
        var claimId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (claimId is null) return Task.CompletedTask;
        var userId = Guid.Parse(claimId.Value);

        if (userId == resource.OwnerUserId) context.Succeed(ownerRequirement);

        return Task.CompletedTask;
    }
}