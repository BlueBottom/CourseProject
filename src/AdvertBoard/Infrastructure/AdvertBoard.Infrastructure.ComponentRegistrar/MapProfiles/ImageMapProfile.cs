using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Domain.Contexts.Images;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class ImageMapProfile : Profile
{
    public ImageMapProfile()
    {
        CreateMap<CreateImageDto, Image>(MemberList.None)
            .ForMember(x => x.Content, map => map.MapFrom(s => s.File))
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<Image, ImageDto>(MemberList.None);
    }
}