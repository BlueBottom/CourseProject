using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Contracts.Contexts.Images;
using AdvertBoard.Domain.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Images;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class AdvertMapProfile : Profile
{
    public AdvertMapProfile()
    {
        CreateMap<AdvertDto, Advert>(MemberList.None);
        
        CreateMap<Advert, ShortAdvertDto>(MemberList.None)
            .ForMember(x => x.ImageId, map => map.MapFrom(x => x.Images.Select(a => a.Id).First()));

        CreateMap<CreateAdvertDto, Advert>(MemberList.None)
            .ForMember(x => x.Images, map => map.MapFrom(x => RequestFilesToImages(x.Images)))
            .ForMember(x => x.CreatedAt, map => map.MapFrom(x => DateTime.UtcNow));

        CreateMap<UpdateAdvertDto, Advert>(MemberList.None)
            .ForMember(x => x.Images, map => map.MapFrom(x => RequestFilesToImages(x.Images)));

        CreateMap<CreateAdvertDto, AdvertDto>(MemberList.None)
            .ForMember(x => x.ImageIds, map => map.MapFrom(x => RequestFilesToImages(x.Images)));

        CreateMap<IFormFile, ImageDto>(MemberList.None)
            .ForMember(x => x.Content, map => map.MapFrom(s => RequestFileToImage(s)));
    }
    
    private IEnumerable<Image> RequestFilesToImages(IEnumerable<IFormFile> files)
    {
        foreach (var file in files)
        {
            var bytes = RequestFileToImage(file);
            yield return new Image()
            {
                Id = default,
                Advert = null,
                AdvertId = default,
                Content = bytes
            };
        }
    }

    private static byte[] RequestFileToImage(IFormFile file)
    {
        using var fileStream = file.OpenReadStream();
        byte[] bytes = new byte[file.Length];
        fileStream.Read(bytes, 0, (int)file.Length);
        return bytes;
    }
}