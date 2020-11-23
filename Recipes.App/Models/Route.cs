namespace Recipes.App.Models
{
    public static class Route
    {
        public static class App
        {
            public const string Categories = "/categories";
            public const string Recipe = "/recipe";
            public const string Recipes = "/recipes";
        }

        public static class Api
        {
            public static class Categories
            {
                public const string Base = "api/categories";
                public const string Recipes = "api/categories/recipes";
                //public const string RecipesCountAll = "api/categories/recipescount/all";
            }

            public static class Recipes
            {
                public const string Base = "api/recipes";
                public const string Search = "api/recipes/search";
            }
        }
    }
}