using System;
using System.Collections.Generic;

namespace Recipes.App.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Firstname { get; set; } = "";
        public string Lastname { get; set; } = "";
        public string Email { get; set; } = "";
        public List<string> Roles { get; set; } = new List<string>();

        public bool IsAdmin
        {
            get
            {
                return Roles?.Exists(r => r.Equals("admin", StringComparison.OrdinalIgnoreCase)) == true;
            }
        }
    }
}