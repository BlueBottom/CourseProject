using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using AdvertBoard.Contracts.Contexts.Reviews.Responses;
using AdvertBoard.Domain.Contexts.Reviews;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class ReviewMapProfile : Profile
{
    public ReviewMapProfile()
    {
        CreateMap<CreateReviewRequest, Review>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<UpdateReviewRequest, Review>(MemberList.None);

        CreateMap<Review, ReviewResponse>(MemberList.None);

        CreateMap<Review, ShortReviewResponse>(MemberList.None)
            .ForMember(x => x.OwnerUser, map => map.MapFrom(s => s.OwnerUser));
    }
}