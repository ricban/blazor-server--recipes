using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recipes.Entities;
using Recipes.Entities.Models;
using Recipes.Repositories.Interfaces;

namespace Recipes.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Category> GetCategoryById(int categoryId)
        {
            return FindByCondition(c => c.CategoryId == categoryId);
        }

        public IQueryable<Category> GetAllCategories()
        {
            return FindAll();
        }

        public IQueryable<Category> GetAllCategoriesWithRecipes()
        {
            return FindAll().Include(i => i.Recipes);
        }
    }
}