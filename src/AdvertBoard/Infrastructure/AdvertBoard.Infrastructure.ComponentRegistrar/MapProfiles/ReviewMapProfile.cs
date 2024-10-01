using AdvertBoard.Contracts.Contexts.Reviews;
using AdvertBoard.Domain.Contexts.Reviews;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class ReviewMapProfile : Profile
{
    public ReviewMapProfile()
    {
        CreateMap<CreateReviewDto, Review>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<UpdateReviewDto, Review>(MemberList.None);

        CreateMap<Review, ReviewDto>(MemberList.None);

        CreateMap<Review, ShortReviewDto>(MemberList.None)
            .ForMember(x => x.OwnerUser, map => map.MapFrom(s => s.OwnerUser));
    }
}