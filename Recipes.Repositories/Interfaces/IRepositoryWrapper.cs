using System.Threading.Tasks;

namespace Recipes.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; }
        IRecipeRepository Recipe { get; }
        void Save();
        Task SaveAsync();
    }
}