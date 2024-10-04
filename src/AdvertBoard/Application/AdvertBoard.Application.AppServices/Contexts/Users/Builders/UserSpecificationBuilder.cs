using AdvertBoard.Application.AppServices.Contexts.Users.Specifications;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Builders;

/// <inheritdoc/>
public class UserSpecificationBuilder : IUserSpecificationBuilder
{
    /// <inheritdoc/>
    public ISpecification<User> Build(GetAllUsersRequest getAllUsersRequest)
    {
        ISpecification<User> specification = Specification<User>.True;

        if (getAllUsersRequest.CreatedFromDate.HasValue)
        {
            specification = specification.And(new StartDateSpecification(getAllUsersRequest.CreatedFromDate.Value));
        }

        if (getAllUsersRequest.CreateToDate.HasValue)
        {
            specification = specification.And(new EndDateSpecification(getAllUsersRequest.CreateToDate.Value));
        }

        if (getAllUsersRequest.MinRating.HasValue)
        {
            specification = specification.And(new MinRatingSpecification(getAllUsersRequest.MinRating.Value));
        }

        if (getAllUsersRequest.MaxRating.HasValue)
        {
            specification = specification.And(new MaxRatingSpecification(getAllUsersRequest.MaxRating.Value));
        }

        if (!string.IsNullOrWhiteSpace(getAllUsersRequest.SearchEmailString))
        {
            specification = specification.And(new EmailSpecification(getAllUsersRequest.SearchEmailString));
        }
        
        if (!string.IsNullOrWhiteSpace(getAllUsersRequest.SearchNameString))
        {
            specification = specification.And(new NameSpecification(getAllUsersRequest.SearchNameString));
        }
        
        if (!string.IsNullOrWhiteSpace(getAllUsersRequest.SearchPhoneString))
        {
            specification = specification.And(new PhoneSpecification(getAllUsersRequest.SearchPhoneString));
        }

        return specification;
    }
}