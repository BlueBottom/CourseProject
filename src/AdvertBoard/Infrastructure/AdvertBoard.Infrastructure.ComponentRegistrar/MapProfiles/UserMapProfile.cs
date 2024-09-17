using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Domain.Contexts.Users;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class UserMapProfile : Profile
{
    public UserMapProfile()
    {
        CreateMap<UserDto, User>(MemberList.None);

        CreateMap<ShortUserDto, User>(MemberList.None);

        CreateMap<User, ShortUserDto>(MemberList.None);

        CreateMap<RegisterUserDto, User>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));
    }
}