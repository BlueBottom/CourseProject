using AdvertBoard.Contracts.Contexts.Comments.Requests;
using AdvertBoard.Contracts.Contexts.Comments.Responses;
using AdvertBoard.Domain.Contexts.Comments;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class CommentMapProfile : Profile
{
    public CommentMapProfile()
    {
        CreateMap<CreateCommentRequest, Comment>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<UpdateCommentRequest, Comment>(MemberList.None)
            .ForMember(x => x.EditedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<Comment, CommentResponse>(MemberList.None);

        CreateMap<Comment, ShortCommentResponse>(MemberList.None)
            .ForMember(x => x.User, map => map.MapFrom(s => s.User))
            .ForMember(x => x.ChildrenAmount, map => map.MapFrom(s => (s.Children != null) ? s.Children.Count : 0));
        CreateMap<Comment, CommentHierarchyResponse>(MemberList.None);
    }
}