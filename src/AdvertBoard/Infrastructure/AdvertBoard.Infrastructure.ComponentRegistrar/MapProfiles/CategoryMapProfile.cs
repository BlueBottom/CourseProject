using AdvertBoard.Contracts.Contexts.Categories.Requests;
using AdvertBoard.Contracts.Contexts.Categories.Responses;
using AdvertBoard.Domain.Contexts.Categories;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class CategoryMapProfile : Profile
{
    public CategoryMapProfile()
    {
        CreateMap<Category, ShortCategoryResponse>(MemberList.None);

        CreateMap<CreateCategoryRequest, Category>(MemberList.None)
            .ForMember(x => x.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<UpdateCategoryRequest, Category>(MemberList.None);
        
        CreateMap<Category, CategoryHierarchyResponse>(MemberList.None);
    }
}