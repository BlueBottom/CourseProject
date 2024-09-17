using AdvertBoard.Contracts.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Images;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class AdvertMapProfile : Profile
{
    public AdvertMapProfile()
    {
        CreateMap<Advert, AdvertDto>(MemberList.None)
            .ForMember(x => x.ImageIds, map => map.MapFrom(s => s.Images.Select(x => x.Id)))
            .ForMember(x => x.CommentIds, map => map.MapFrom(s => s.Comments.Select(x => x.Id)))
            .ForMember(x => x.CategoryId, map => map.MapFrom(s => s.CategoryId));
            
        CreateMap<AdvertDto, Advert>(MemberList.None);
        
        CreateMap<Advert, ShortAdvertDto>(MemberList.None)
            .ForMember(x => x.ImageId, map => map.MapFrom(x => x.Images.Select(a => a.Id).First()));

        CreateMap<CreateAdvertDto, Advert>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(x => DateTime.UtcNow));

        CreateMap<UpdateAdvertDto, Advert>(MemberList.None);

        CreateMap<CreateAdvertDto, AdvertDto>(MemberList.None);
    }
}