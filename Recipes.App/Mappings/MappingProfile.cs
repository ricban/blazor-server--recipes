using AutoMapper;
using Recipes.App.ViewModels;
using Recipes.Core.Models;
using Recipes.Core.Request;
using Recipes.Core.Response;

namespace Recipes.App.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ViewModel to Request

            CreateMap<Recipe, RecipeRequest>();
            CreateMap<Search, RecipeSearchRequest>();

            // Response to ViewModel

            CreateMap<CategoryKeyValueResponse, CategoryKeyValue>();
            CreateMap<CategoryWithRecipesResponse, CategoryWithRecipes>();

            CreateMap<RecipeResponse, Recipe>();
            CreateMap<PagedData<RecipeResponse>, PagedData<Recipe>>();
        }
    }
}