using AutoMapper;
using Domain.Entitites.Categories;
using DTO.Categories;

namespace API.Mappings.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryResponseDto>()
                .ReverseMap()
                .ForAllMembers(x => x.Condition(
                    (src, dest, property) =>
                    {
                        // Let's ignore both null and empty string properties
                        if (property == null) return false;
                        if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;

                        return true;
                    }));
        }
    }
}
