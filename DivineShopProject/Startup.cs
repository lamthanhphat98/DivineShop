using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DivineShopProject.Interfaces;
using DivineShopProject.Models;
using DivineShopProject.Reposity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DivineShopProject
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
            services.AddDbContext<DbConnection>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStr")));
            // services.UseSession();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddTransient<IProduct, ProductReposity>();
            services.AddTransient<IUser, UserReposity>();
            services.AddTransient<ICart, CartReposity>();
            services.AddTransient<IOrder, OrderReposity>();
            services.AddTransient<ICategory, CategoryReposity>();
            services.AddTransient<ILike, LikeReposity>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
               
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=List}/{id?}");
            });
        }
    }
}
