using AdvertBoard.Contracts.Contexts.Comments;
using AdvertBoard.Domain.Contexts.Comments;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class CommentMapProfile : Profile
{
    public CommentMapProfile()
    {
        CreateMap<CreateCommentDto, Comment>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<UpdateCommentDto, Comment>(MemberList.None)
            .ForMember(x => x.EditedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<Comment, CommentDto>(MemberList.None);

        CreateMap<Comment, ShortCommentDto>(MemberList.None)
            .ForMember(x => x.User, map => map.MapFrom(s => s.User))
            .ForMember(x => x.ChildrenAmount, map => map.MapFrom(s => (s.Children != null) ? s.Children.Count : 0));
        CreateMap<Comment, CommentHierarchyDto>(MemberList.None);
    }
}