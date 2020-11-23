namespace Recipes.App.ViewModels
{
    public class CategoryWithRecipes
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public int RecipesCount { get; set; }
    }
}