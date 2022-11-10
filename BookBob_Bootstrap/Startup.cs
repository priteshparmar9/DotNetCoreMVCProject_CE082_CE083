using BookBob_Bootstrap.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap
{
    public class Startup
    {
        private IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContextPool<AppDbContext>(
               options => options.UseSqlServer(configuration.GetConnectionString("BookBobDBConnection")));
            services.AddControllersWithViews();
            services.AddScoped<IBookRepository, SQLBookRepository>();
            services.AddScoped<IUserRepository, SQLUserRepository>();
            services.AddScoped<ICartRepository, SQLCartRepository>();
            services.AddScoped<ICartItemRepository, SQLCartItemRepository>();
            services.AddSession(
                o =>
                {
                    o.IdleTimeout = TimeSpan.FromSeconds(1000 * 60 * 60);
                }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                
                endpoints.MapGet("/", async context =>
               {
                    context.Response.Redirect("/user");
               });                   
            });
        }
    }
}
