using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Recipes.App.Models
{
    public class ApplicationState
    {
        public ApplicationState(IConfiguration configuration, IMemoryCache cache)
        {
            Cache = cache;
            Defaults = configuration.GetSection(nameof(Defaults)).Get<Defaults>();
        }

        public User CurrentUser { get; set; } = new User
        {
            Id = 1,
            Username = "RBB",
            Firstname = "RICKY",
            Lastname = "BANCOLITA",
            Email = "ricky.bancolita@gmail.com",
            Roles = new List<string> { "ADMIN" }
        };

        public Defaults Defaults { get; set; }
        public IMemoryCache Cache { get; set; }

        public bool IsUserAuthenticated
        {
            get
            {
                return CurrentUser != null;
            }
        }
    }
}