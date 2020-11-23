using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Recipes.Core.Models;
using Recipes.Core.Response;
using Recipes.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [FormatFilter]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepositoryWrapper Repository;
        private readonly IMapper Mapper;
        private readonly IMemoryCache Cache;

        public CategoriesController(IRepositoryWrapper repository, IMapper mapper, IMemoryCache cache)
        {
            Repository = repository;
            Mapper = mapper;
            Cache = cache;
        }

        // GET: api/<controller>/{categoryId:int}.{format?}
        [HttpGet("{categoryId:int}.{format?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CategoriesAsync(int categoryId)
        {
            var entity = await Cache.GetOrCreateAsync($"{CacheKey.Categories}_{categoryId}", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);

                return await Repository.Category.GetCategoryById(categoryId)
                    .Select(s => Mapper.Map<CategoryResponse>(s))
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

            }).ConfigureAwait(false);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        // GET: api/<controller>/<action>/{format?}
        [HttpGet("{format?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CategoriesAsync()
        {
            return Ok(await Cache.GetOrCreateAsync(CacheKey.Categories, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);

                return await Repository.Category.GetAllCategories()
                    .Select(s => Mapper.Map<CategoryKeyValueResponse>(s))
                    .ToListAsync()
                    .ConfigureAwait(false);

            }).ConfigureAwait(false));
        }

        // GET: api/<controller>/<action>/{format?}
        [HttpGet("[action]/{format?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RecipesAsync()
        {
            return Ok(await Cache.GetOrCreateAsync(CacheKey.CategoriesWithRecipes, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);

                return await Repository.Category.GetAllCategoriesWithRecipes()
                    .Where(w => w.Recipes.Count > 0)
                    .Select(s => Mapper.Map<CategoryWithRecipesResponse>(s))
                    .ToListAsync()
                    .ConfigureAwait(false);

            }).ConfigureAwait(false));
        }
    }
}