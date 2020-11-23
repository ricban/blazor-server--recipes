using System.ComponentModel.DataAnnotations;

namespace Recipes.App.ViewModels
{
    public class Search
    {
        [Required]
        public string Type { get; set; } = "";

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Text { get; set; } = "";

        public int TakeCount { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}