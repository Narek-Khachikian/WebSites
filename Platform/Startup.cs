using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Platform
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MessageOptions>(opt => opt.CityName = "Moscow");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<MessageOptions> msgOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapWhen(context => context.Request.Query["custom"] == "true", app =>
                app.UseMiddleware<QueryStringMiddleWare>());

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/location")
            //    {
            //        MessageOptions options = msgOptions.Value;
            //        await context.Response.WriteAsync($"City Name: {options.CityName}, Country:{options.CountryName}\n");
            //    }
            //    await next();
            //});

            //app.Use(async (context, next) => {
            //    if (context.Request.Method == HttpMethods.Get
            //    && context.Request.Query["custom"] == "true")
            //    {
            //        await context.Response.WriteAsync("Custom Middleware \n");
            //    }
            //    await next();
            //    await context.Response.WriteAsync("\nafter next text");
            //});

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Query.ContainsKey("Short"))
            //    {
            //        await context.Response.WriteAsync("Short request");
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});

            //app.MapWhen(context => context.Request.Query.ContainsKey("Hello"), c => c.Use(async (context, next) =>
            //  {
            //      await context.Response.WriteAsync("Hello added");

            //  }));

            //app.Map("/branch", branch =>
            //{
            //    branch.UseMiddleware<QueryStringMiddleWare>();
            //    branch.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Branched text");

            //    });
            //});

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Query["requests"] == "true")
            //    {
            //        await context.Response.WriteAsync("additional info \n");
            //    }
            //    await next();
            //    await context.Response.WriteAsync($"\nStatus: {context.Response.StatusCode}");
            //});

            //app.UseMiddleware<QueryStringMiddleWare>("Narek");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
