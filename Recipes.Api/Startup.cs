using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recipes.Api.Mappings;
using Recipes.Api.Models;
using Recipes.Entities;
using Recipes.Repositories;
using Recipes.Repositories.Interfaces;

namespace Recipes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // AutoMapper

            var automapperConfiguration = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            services.AddSingleton(automapperConfiguration.CreateMapper());
            services.AddAutoMapper(typeof(Startup));

            // Memory Cache

            services.AddMemoryCache();

            //
            //services.AddControllers();
            services.AddControllers().AddXmlSerializerFormatters();

            // EF Core - SQL Server

            services.AddDbContext<RepositoryContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString(ConnectionString.Default));
                options.EnableSensitiveDataLogging();
            });

            // Dependency Injection

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            //services.AddSingleton<IMemoryCache>(_ => new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/error");
            //}

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}