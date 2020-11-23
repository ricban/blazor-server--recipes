namespace Recipes.Core.Models
{
    public static class CacheKey
    {
        public static string CancelTokenSource
        {
            get { return "_CancelTokenSource"; }
        }

        public static string Category
        {
            get { return "_Category"; }
        }

        public static string Categories
        {
            get { return "_Categories"; }
        }

        public static string CategoriesWithRecipes
        {
            get { return "_CategoriesWithRecipes"; }
        }
    }
}