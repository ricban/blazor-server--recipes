using System.Linq;
using Recipes.Entities.Models;

namespace Recipes.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        IQueryable<Category> GetCategoryById(int categoryId);
        IQueryable<Category> GetAllCategories();
        IQueryable<Category> GetAllCategoriesWithRecipes();
    }
}