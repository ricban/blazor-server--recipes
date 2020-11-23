using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Recipes.Core.Enumerations;
using Recipes.Core.Models;
using Recipes.Core.Request;
using Recipes.Core.Response;
using Recipes.Entities.Models;
using Recipes.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [FormatFilter]
    public class RecipesController : ControllerBase
    {
        private readonly IRepositoryWrapper Repository;
        private readonly IMapper Mapper;
        private readonly IMemoryCache Cache;

        public RecipesController(IRepositoryWrapper repository, IMapper mapper, IMemoryCache cache)
        {
            Repository = repository;
            Mapper = mapper;
            Cache = cache;
        }

        // GET: api/<controller>/{id}.{format?}
        [HttpGet("{id:int}.{format?}", Name = "recipes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RecipesAsync([FromRoute]int id)
        {
            var entity = await Repository.Recipe.GetRecipeById(id)
                .Select(s => Mapper.Map<RecipeResponse>(s))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (entity == null)
            {
                return NotFound(id);
            }

            return Ok(entity);
        }

        // GET api/<controller>/<action>/{format?}
        [HttpGet("[action]/{format?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SearchAsync([FromBody]RecipeSearchRequest request)
        {
            var query = default(IQueryable<Recipe>);

            switch(request.Type)
            {
                case SearchMode.RecipeCategory:
                    query = Repository.Recipe.GetRecipeByCategoryId(int.Parse(request.Text));
                    break;
                case SearchMode.RecipeName:
                    query = Repository.Recipe.GetRecipeByName(request.Text);
                    break;
                case SearchMode.RecipeIngredient:
                    query = Repository.Recipe.GetRecipeByIngredient(request.Text);
                    break;
                case SearchMode.RecipeAuthor:
                    query = Repository.Recipe.GetRecipeByAuthor(request.Text);
                    break;
                case SearchMode.LatestRecipes:
                    query = Repository.Recipe.GetLatestRecipes().Take(request.TakeCount);
                    break;
            }

            var source = await query
                .Select(s => Mapper.Map<RecipeResponse>(s))
                .ToListAsync()
                .ConfigureAwait(false);

            return Ok(PagedData<RecipeResponse>.Create(source, request.PageNumber, request.PageSize));
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody]RecipeRequest request)
        {
            var entity = Mapper.Map<Recipe>(request);

            Repository.Recipe.Create(entity);
            Repository.Save();

            Cache.Remove(CacheKey.CategoriesWithRecipes);

            return CreatedAtAction(nameof(Recipes), new { id = entity.RecipeId }, Mapper.Map<RecipeResponse>(entity));
        }

        // PUT api/<controller>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RecipeRequest), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateAsync([FromBody]RecipeRequest request)
        {
            var entity = await Repository.Recipe
                .GetRecipeById(request.RecipeId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (entity == null)
            {
                return NotFound(request);
            }
            else
            {
                entity = Mapper.Map<Recipe>(request);
            }

            Repository.Recipe.Update(entity);
            Repository.Save();

            Cache.Remove(CacheKey.CategoriesWithRecipes);

            return Ok(entity);
        }

        // DELETE api/<controller>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteAsync([FromRoute]int id)
        {
            var entity = await Repository.Recipe
                .GetRecipeById(id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (entity == null)
            {
                return NotFound(id);
            }

            Repository.Recipe.Delete(entity);
            Repository.Save();

            Cache.Remove(CacheKey.CategoriesWithRecipes);

            return Ok(entity);
        }
    }
}