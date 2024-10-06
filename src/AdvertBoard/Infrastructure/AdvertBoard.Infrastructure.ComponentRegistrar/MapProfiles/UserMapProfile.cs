using AdvertBoard.Application.AppServices.Contexts.Users.Models;
using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using AdvertBoard.Contracts.Contexts.Users.Responses;
using AdvertBoard.Domain.Contexts.Users;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class UserMapProfile : Profile
{
    public UserMapProfile()
    {
        CreateMap<UpdateUserRequest, User>(MemberList.None);

        CreateMap<User, UserResponse>(MemberList.None);

        CreateMap<User, UserWithPasswordModel>(MemberList.None);

        CreateMap<ShortUserResponse, User>(MemberList.None);

        CreateMap<User, ShortUserResponse>(MemberList.None);

        CreateMap<RegisterUserRequest, User>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));
    }
}