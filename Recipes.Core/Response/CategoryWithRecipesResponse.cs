namespace Recipes.Core.Response
{
    public class CategoryWithRecipesResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public int RecipesCount { get; set; }
    }
}