using AdvertBoard.Application.AppServices.Contexts.Users.Specifications;
using AdvertBoard.Application.AppServices.Specifications;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Builders;

/// <inheritdoc/>
public class UserSpecificationBuilder : IUserSpecificationBuilder
{
    /// <inheritdoc/>
    public ISpecification<User> Build(GetAllUsersDto getAllUsersDto)
    {
        ISpecification<User> specification = Specification<User>.True;

        if (getAllUsersDto.CreatedFromDate.HasValue)
        {
            specification = specification.And(new StartDateSpecification(getAllUsersDto.CreatedFromDate.Value));
        }

        if (getAllUsersDto.CreateToDate.HasValue)
        {
            specification = specification.And(new EndDateSpecification(getAllUsersDto.CreateToDate.Value));
        }

        if (getAllUsersDto.MinRating.HasValue)
        {
            specification = specification.And(new MinRatingSpecification(getAllUsersDto.MinRating.Value));
        }

        if (getAllUsersDto.MaxRating.HasValue)
        {
            specification = specification.And(new MaxRatingSpecification(getAllUsersDto.MaxRating.Value));
        }

        if (!string.IsNullOrWhiteSpace(getAllUsersDto.SearchEmailString))
        {
            specification = specification.And(new EmailSpecification(getAllUsersDto.SearchEmailString));
        }
        
        if (!string.IsNullOrWhiteSpace(getAllUsersDto.SearchNameString))
        {
            specification = specification.And(new NameSpecification(getAllUsersDto.SearchNameString));
        }
        
        if (!string.IsNullOrWhiteSpace(getAllUsersDto.SearchPhoneString))
        {
            specification = specification.And(new PhoneSpecification(getAllUsersDto.SearchPhoneString));
        }

        return specification;
    }
}