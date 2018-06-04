using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartDAL.EF;
using ShoppingCartDAL.Initializers;
using ShoppingCartDAL.Repos;
using ShoppingCartServices;

namespace ShoppingCartWebsite
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
            services.AddSingleton(_ => Configuration);
            services.AddDbContext<ShoppingCartDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("ShoppingCartDb")));

            services.AddMvc();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShoppingCartRepo, ShoppingCartRepo>();
            services.AddScoped<IPriceCalculator, PriceCalculator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();

                using (IServiceScope serviceScope = app.ApplicationServices
                    .GetService<IServiceScopeFactory>().CreateScope())
                {
                    //ShoppingCartInitializer.InitializeData(serviceScope);
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
