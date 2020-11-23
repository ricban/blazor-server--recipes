using System.Linq;
using Recipes.Entities.Models;

namespace Recipes.Repositories.Interfaces
{
    public interface IRecipeRepository : IRepositoryBase<Recipe>
    {
        IQueryable<Recipe> GetRecipeById(int recipeId);
        IQueryable<Recipe> GetRecipeByCategoryId(int categoryId);
        IQueryable<Recipe> GetRecipeByName(string recipeName);
        IQueryable<Recipe> GetRecipeByIngredient(string ingredient);
        IQueryable<Recipe> GetRecipeByAuthor(string author);
        IQueryable<Recipe> GetLatestRecipes();
    }
}