using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recipes.Entities;
using Recipes.Entities.Models;
using Recipes.Repositories.Interfaces;

namespace Recipes.Repositories
{
    public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Recipe> GetRecipeById(int recipeId)
        {
            return FindByCondition(c => c.RecipeId == recipeId)
                .Include(i => i.Category);
        }

        public IQueryable<Recipe> GetRecipeByCategoryId(int categoryId)
        {
            return FindAll()
                .Include(i => i.Category)
                .Where(w => w.CategoryId == categoryId)
                .OrderBy(o => o.RecipeAuthor)
                .ThenBy(t => t.RecipeName);
        }

        public IQueryable<Recipe> GetRecipeByName(string recipeName)
        {
            return FindAll()
                .Include(i => i.Category)
                .Where(w => EF.Functions.Like(w.RecipeName, recipeName))
                .OrderBy(o => o.RecipeAuthor)
                .ThenBy(t => t.RecipeName);
        }

        public IQueryable<Recipe> GetRecipeByIngredient(string ingredient)
        {
            return FindAll()
                .Include(i => i.Category)
                .Where(w => EF.Functions.Like(w.RecipeIngredients, ingredient))
                .OrderBy(o => o.RecipeAuthor)
                .ThenBy(t => t.RecipeName);
        }

        public IQueryable<Recipe> GetRecipeByAuthor(string author)
        {
            return FindAll()
                .Include(i => i.Category)
                .Where(w => EF.Functions.Like(w.RecipeAuthor, author))
                .OrderBy(o => o.RecipeAuthor)
                .ThenBy(t => t.RecipeName);
        }

        public IQueryable<Recipe> GetLatestRecipes()
        {
            return FindAll()
                .Include(i => i.Category)
                .OrderByDescending(o => o.CreationDate)
                .ThenBy(t => t.RecipeAuthor)
                .ThenBy(t => t.RecipeName);
        }
    }
}