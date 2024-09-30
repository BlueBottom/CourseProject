using AdvertBoard.Contracts.Contexts.Categories;
using AdvertBoard.Domain.Contexts.Categories;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class CategoryMapProfile : Profile
{
    public CategoryMapProfile()
    {
        CreateMap<Category, ShortCategoryDto>(MemberList.None);

        CreateMap<CreateCategoryDto, Category>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<UpdateCategoryDto, Category>(MemberList.None);

        CreateMap<Category, Category>()
            .ForMember(x => x.CreatedAt, map => map.Ignore())
            .ForMember(x => x.ParentId, map => map.Ignore())
            .ForMember(x => x.Id, map => map.Ignore())
            .ForMember(x => x.Children, map => map.Ignore())
            .ForMember(x => x.Adverts, map => map.Ignore());

        CreateMap<Category, CategoryHierarchyDto>(MemberList.None);
    }
}