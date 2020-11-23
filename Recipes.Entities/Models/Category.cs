using System;
using System.Collections.Generic;

namespace Recipes.Entities.Models
{
    public class Category
    {
        public Category()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; } = "";
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; } = "";

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}