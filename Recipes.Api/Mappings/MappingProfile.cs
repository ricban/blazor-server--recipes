using AutoMapper;
using Recipes.Core.Request;
using Recipes.Core.Response;
using Recipes.Entities.Models;

namespace Recipes.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to Response

            CreateMap<Category, CategoryKeyValueResponse>();
            CreateMap<Category, CategoryWithRecipesResponse>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<Recipe, RecipeResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            // Request to Entity

            CreateMap<RecipeRequest, Recipe>();

            //CreateMap<RecipeResponse, Recipe>();
        }
    }
}