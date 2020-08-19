using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advanced.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Advanced
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(Configuration["ConnectionStrings:MainDatabase"]);
                opt.EnableSensitiveDataLogging(true);
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddDbContext<IdentityContext>(opt =>
            {
                opt.UseSqlServer(Configuration["ConnectionStrings:IdentityDatabase"]);
            });
            services.AddIdentity<IdentityUser, IdentityRole>(opt => 
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 10;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<IdentityContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "controllers/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });

            SeedData.SeedDatabase(dbContext);
        }
    }
}
