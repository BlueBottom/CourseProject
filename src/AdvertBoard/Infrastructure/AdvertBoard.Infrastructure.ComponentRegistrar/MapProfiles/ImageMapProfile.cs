using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Domain.Contexts.Images;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class ImageMapProfile : Profile
{
    public ImageMapProfile()
    {
        CreateMap<ImageDto, Image>(MemberList.None);
    }
}