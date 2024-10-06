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
    public ISpecification<User> Build(GetAllUsersByFilterRequest getAllUsersByFilterRequest)
    {
        ISpecification<User> specification = Specification<User>.True;

        if (getAllUsersByFilterRequest.CreatedFromDate.HasValue)
        {
            specification = specification.And(new StartDateSpecification(getAllUsersByFilterRequest.CreatedFromDate.Value));
        }

        if (getAllUsersByFilterRequest.CreateToDate.HasValue)
        {
            specification = specification.And(new EndDateSpecification(getAllUsersByFilterRequest.CreateToDate.Value));
        }

        if (getAllUsersByFilterRequest.MinRating.HasValue)
        {
            specification = specification.And(new MinRatingSpecification(getAllUsersByFilterRequest.MinRating.Value));
        }

        if (getAllUsersByFilterRequest.MaxRating.HasValue)
        {
            specification = specification.And(new MaxRatingSpecification(getAllUsersByFilterRequest.MaxRating.Value));
        }

        if (!string.IsNullOrWhiteSpace(getAllUsersByFilterRequest.SearchEmailString))
        {
            specification = specification.And(new EmailSpecification(getAllUsersByFilterRequest.SearchEmailString));
        }
        
        if (!string.IsNullOrWhiteSpace(getAllUsersByFilterRequest.SearchNameString))
        {
            specification = specification.And(new NameSpecification(getAllUsersByFilterRequest.SearchNameString));
        }
        
        if (!string.IsNullOrWhiteSpace(getAllUsersByFilterRequest.SearchPhoneString))
        {
            specification = specification.And(new PhoneSpecification(getAllUsersByFilterRequest.SearchPhoneString));
        }

        return specification;
    }
}