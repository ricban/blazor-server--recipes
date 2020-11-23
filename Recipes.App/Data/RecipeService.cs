using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.Core.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Recipes.App.Models;
using Recipes.App.ViewModels;
using Recipes.Core.Enumerations;
using Recipes.Core.Models;
using Recipes.Core.Request;
using Recipes.Core.Response;

namespace Recipes.App.Data
{
    public class RecipeService
    {
        private readonly IMapper Mapper;
        private readonly HttpClient Client;
        private readonly ApplicationState State;

        public RecipeService(HttpClient client, ApplicationState state, IMapper mapper)
        {
            Client = client;
            State = state;
            Mapper = mapper;
        }

        public async Task<List<CategoryKeyValue>> GetCategoriesAsync()
        {
            Uri requestURI = default!;
            HttpResponseMessage response = default!;

            try
            {
                return await State.Cache.GetOrCreateAsync(CacheKey.Categories, async entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(30);

                    requestURI = new Uri(Client.BaseAddress + Route.Api.Categories.Base);
                    response = await Client.GetAsync(requestURI).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return Mapper.Map<List<CategoryKeyValue>>(content.Deserialize<List<CategoryKeyValueResponse>>());

                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex?.Data.Add("Uri", requestURI?.AbsoluteUri);
                ex?.Data.Add("Response", $"{response?.StatusCode} {response?.ReasonPhrase}".Trim());

                throw;
            }
        }

        public async Task<List<CategoryWithRecipes>> GetCategoriesWithRecipesAsync()
        {
            Uri requestURI = default!;
            HttpResponseMessage response = default!;

            try
            {
                return await State.Cache.GetOrCreateAsync(CacheKey.CategoriesWithRecipes, async entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(30);

                    requestURI = new Uri(Client.BaseAddress + Route.Api.Categories.Recipes);
                    response = await Client.GetAsync(requestURI).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return Mapper.Map<List<CategoryWithRecipes>>(content.Deserialize<List<CategoryWithRecipesResponse>>());

                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex?.Data.Add("Uri", requestURI?.AbsoluteUri);
                ex?.Data.Add("Response", $"{response?.StatusCode} {response?.ReasonPhrase}".Trim());

                throw;
            }
        }

        public async Task<PagedData<Recipe>> GetRecipesByCategoryIdAsync(int categoryId)
        {
            var parameters = new RecipeSearchRequest
            {
                Type = SearchMode.LatestRecipes,
                Text = $"{categoryId}",
                PageNumber = 1,
                PageSize = State.Defaults.PageSize,
                TakeCount = State.Defaults.PageSize,
            };

            return await SearchRecipesAsync(parameters).ConfigureAwait(false);
        }

        public async Task<PagedData<Recipe>> SearchRecipesAsync(Search search)
        {
            var parameters = Mapper.Map<RecipeSearchRequest>(search);

            return await SearchRecipesAsync(parameters).ConfigureAwait(false);
        }

        public async Task<PagedData<Recipe>> SearchRecipesAsync(RecipeSearchRequest parameters)
        {
            Uri requestURI = default!;
            HttpResponseMessage response = default!;

            try
            {
                requestURI = new Uri(Client.BaseAddress + Route.Api.Recipes.Search);

                var request = new HttpRequestMessage(HttpMethod.Get, requestURI)
                {
                    Content = new StringContent(parameters.Serialize(), Encoding.UTF8, MimeType.Json)
                };

                response = await Client.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return Mapper.Map<PagedData<Recipe>>(content.Deserialize<PagedData<RecipeResponse>>());
            }
            catch (Exception ex)
            {
                ex?.Data.Add("Uri", requestURI?.AbsoluteUri);
                ex?.Data.Add("Parameters", parameters.Serialize(true));
                ex?.Data.Add("Response", $"{response?.StatusCode} {response?.ReasonPhrase}".Trim());

                throw;
            }
        }

        public async Task<Recipe> GetRecipeByIdAsync(int recipeId)
        {
            var data = new Recipe();

            if (recipeId > 0)
            {
                Uri requestURI = default!;
                HttpResponseMessage response = default!;

                try
                {
                    requestURI = new Uri(Client.BaseAddress + Route.Api.Recipes.Base + $"/{recipeId}");
                    response = await Client.GetAsync(requestURI).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    data = Mapper.Map<Recipe>(content.Deserialize<RecipeResponse>());
                }
                catch (Exception ex)
                {
                    ex?.Data.Add("Uri", requestURI?.AbsoluteUri);
                    ex?.Data.Add("Response", $"{response?.StatusCode} {response?.ReasonPhrase}".Trim());

                    throw;
                }
            }

            return data;
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            Uri requestURI = default!;
            HttpResponseMessage response = default!;

            recipe.CreatedBy = State.CurrentUser.Username;
            recipe.CreationDate = DateTime.Now;
            recipe.UpdatedBy = recipe.CreatedBy;
            recipe.LastUpdated = recipe.CreationDate;

            var parameters = Mapper.Map<RecipeRequest>(recipe);

            try
            {
                requestURI = new Uri(Client.BaseAddress + Route.Api.Recipes.Base);

                var content = new StringContent(parameters.Serialize(), Encoding.UTF8, MimeType.Json);

                response = await Client.PostAsync(requestURI, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var jsonData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                State.Cache.Remove(CacheKey.CategoriesWithRecipes);

                return Mapper.Map<Recipe>(jsonData.Deserialize<RecipeResponse>());
            }
            catch (Exception ex)
            {
                ex?.Data.Add("Uri", requestURI?.AbsoluteUri);
                ex?.Data.Add("Parameters", parameters.Serialize(true));
                ex?.Data.Add("Response", $"{response?.StatusCode} {response?.ReasonPhrase}".Trim());

                throw;
            }
        }

        public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
        {
            Uri requestURI = default!;
            HttpResponseMessage response = default!;

            recipe.UpdatedBy = State.CurrentUser.Username;
            recipe.LastUpdated = DateTime.Now;

            var parameters = Mapper.Map<RecipeRequest>(recipe);

            try
            {
                requestURI = new Uri(Client.BaseAddress + Route.Api.Recipes.Base);

                var content = new StringContent(parameters.Serialize(), Encoding.UTF8, MimeType.Json);

                response = await Client.PutAsync(requestURI, content).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var jsonData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                State.Cache.Remove(CacheKey.CategoriesWithRecipes);

                return Mapper.Map<Recipe>(jsonData.Deserialize<RecipeResponse>());
            }
            catch (Exception ex)
            {
                ex?.Data.Add("Uri", requestURI?.AbsoluteUri);
                ex?.Data.Add("Parameters", parameters.Serialize(true));
                ex?.Data.Add("Response", $"{response?.StatusCode} {response?.ReasonPhrase}".Trim());

                throw;
            }
        }

        public async Task<bool> DeleteRecipeAsync(int recipeId)
        {
            Uri requestURI = default!;
            HttpResponseMessage response = default!;

            try
            {
                requestURI = new Uri(Client.BaseAddress + Route.Api.Recipes.Base + $"/{recipeId}");
                response = await Client.DeleteAsync(requestURI).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                State.Cache.Remove(CacheKey.CategoriesWithRecipes);

                return true;
            }
            catch (Exception ex)
            {
                ex?.Data.Add("Uri", requestURI?.AbsoluteUri);
                ex?.Data.Add("Response", $"{response?.StatusCode} {response?.ReasonPhrase}".Trim());

                throw;
            }
        }
    }
}