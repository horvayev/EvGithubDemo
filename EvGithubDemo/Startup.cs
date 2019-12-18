using BusinessLogic;
using BusinessLogic.Github;
using DataAccess;
using EvGithubDemoWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EvGithubDemo
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
            services.AddControllers();

            // could be registered as a singleton since our simple demo impl uses class level in memory store
            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped<IBusinessContext>(ctx =>
            {
                IDataContext dataContext = ctx.GetService<IDataContext>();
                return new BusinessContext(dataContext);
            });
            services.AddSingleton<ICachingService, CachingService>();
            services.AddSingleton<GithubClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}