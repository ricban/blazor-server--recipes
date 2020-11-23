using System;

namespace Recipes.Core.Response
{
    public class RecipeResponse
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; } = "";
        public string RecipeAuthor { get; set; } = "";
        public int CategoryId { get; set; }
        public string RecipeIngredients { get; set; } = "";
        public string RecipeProcedures { get; set; } = "";
        public string RecipeVideo { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; } = "";
        public string CategoryName { get; set; } = "";
    }
}