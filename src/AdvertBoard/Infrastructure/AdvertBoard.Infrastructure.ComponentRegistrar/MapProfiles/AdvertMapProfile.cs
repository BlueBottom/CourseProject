using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using AdvertBoard.Contracts.Contexts.Adverts.Responses;
using AdvertBoard.Domain.Contexts.Adverts;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class AdvertMapProfile : Profile
{
    public AdvertMapProfile()
    {
        CreateMap<Advert, AdvertResponse>(MemberList.None)
            .ForMember(x => x.ImageIds, map => map.MapFrom(s => s.Images.Select(x => x.Id)))
            .ForMember(x => x.CommentIds, map => map.MapFrom(s => s.Comments.Select(x => x.Id)))
            .ForMember(x => x.CategoryId, map => map.MapFrom(s => s.CategoryId));

        CreateMap<AdvertResponse, Advert>(MemberList.None);

        CreateMap<Advert, ShortAdvertResponse>(MemberList.None)
            .ForMember(x => x.ImageId, map => map.MapFrom(x => x.Images.Select(a => a.Id).First()));

        CreateMap<CreateAdvertRequest, Advert>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(x => DateTime.UtcNow));

        CreateMap<UpdateAdvertRequest, Advert>(MemberList.None);

        CreateMap<CreateAdvertRequest, AdvertResponse>(MemberList.None);
    }
}