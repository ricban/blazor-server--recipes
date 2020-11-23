using System;
using System.Collections.Generic;

namespace Recipes.Entities.Models
{
    public class User
    {
        public User()
        {
            Recipes = new HashSet<Recipe>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}