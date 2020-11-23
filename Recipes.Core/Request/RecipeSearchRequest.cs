using System.ComponentModel.DataAnnotations;
using Recipes.Core.Enumerations;

namespace Recipes.Core.Request
{
    public class RecipeSearchRequest
    {
        [Required]
        public SearchMode Type { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Text { get; set; } = "";

        public int TakeCount { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}