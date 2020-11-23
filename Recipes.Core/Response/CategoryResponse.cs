using System;

namespace Recipes.Core.Response
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; } = "";
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; } = "";
    }
}