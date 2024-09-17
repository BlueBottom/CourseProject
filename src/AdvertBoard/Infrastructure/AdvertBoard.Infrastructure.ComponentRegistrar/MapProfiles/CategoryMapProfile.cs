using AdvertBoard.Contracts.Contexts.Categories;
using AdvertBoard.Domain.Contexts.Categories;
using AutoMapper;

namespace AdvertBoard.Infrastructure.ComponentRegistrar.MapProfiles;

public class CategoryMapProfile : Profile
{
    public CategoryMapProfile()
    {
        CreateMap<Category, ShortCategoryDto>(MemberList.None);
    }
}