using System.Threading.Tasks;
using Recipes.Entities;
using Recipes.Repositories.Interfaces;

namespace Recipes.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext context;
        private ICategoryRepository category;
        private IRecipeRepository recipe;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            context = repositoryContext;
        }

        public ICategoryRepository Category
        {
            get
            {
                return category ??= new CategoryRepository(context);
            }
        }

        public IRecipeRepository Recipe
        {
            get
            {
                return recipe ??= new RecipeRepository(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}