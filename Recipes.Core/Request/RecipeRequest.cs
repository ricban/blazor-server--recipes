using System;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Core.Request
{
    public class RecipeRequest
    {
        public RecipeRequest()
        {
        }

        public RecipeRequest(RecipeRequest source)
        {
            RecipeId = source.RecipeId;
            RecipeName = source.RecipeName;
            RecipeAuthor = source.RecipeAuthor;
            RecipeIngredients = source.RecipeIngredients;
            RecipeProcedures = source.RecipeProcedures;
            RecipeVideo = source.RecipeVideo;
            CategoryId = source.CategoryId;
            CreatedBy = source.CreatedBy;
            CreationDate = source.CreationDate;
            UpdatedBy = source.UpdatedBy;
            LastUpdated = source.LastUpdated;
        }

        [Key]
        public int RecipeId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string RecipeName { get; set; } = "";

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string RecipeAuthor { get; set; } = "";

        [Required]
        [StringLength(5000, MinimumLength = 1)]
        public string RecipeIngredients { get; set; } = "";

        [Required]
        [StringLength(5000, MinimumLength = 1)]
        public string RecipeProcedures { get; set; } = "";

        public string RecipeVideo { get; set; } = "";

        [Required]
        [MinLength(1)]
        public string CategoryId { get; set; } = "";

        public string CreatedBy { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public string UpdatedBy { get; set; } = "";
        public DateTime LastUpdated { get; set; }
    }
}