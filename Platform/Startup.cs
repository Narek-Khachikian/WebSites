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
using Microsoft.AspNetCore.Routing;

namespace Platform
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<MessageOptions>(opt => opt.CityName = "Moscow");
            services.Configure<RouteOptions>(opt =>
            opt.ConstraintMap.Add("countryName",typeof(CountryRouteConstraint))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<MessageOptions> msgOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware<Population>();
            //app.UseMiddleware<Capital>();
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello\n");
            //    await next();
            //});
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                Endpoint end = context.GetEndpoint();
                await context.Response.WriteAsync($"used {end.DisplayName}");
                await next();
            });

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapGet("{first:countryName}/{sec:bool?}/{third:length(10)?}/{*catchall}", async context =>
                {
                    foreach(var item in context.Request.RouteValues)
                    {
                        if(item.Value != null)
                        {
                            await context.Response.WriteAsync($"Key:{item.Key} - Value:{item.Value}\n");
                        }
                    }
                }).WithDisplayName("main Endpoint\n")/*.Add(endpointBuilder => ((RouteEndpointBuilder)endpointBuilder).Order = 1)*/;

                endpoint.MapFallback(async context =>
                {
                    await context.Response.WriteAsync("Error path");
                }).WithDisplayName("fallback endpoint\n");
            });
            //app.UseEndpoints(endpoint =>
            //{
            //    endpoint.MapGet("files/{name}.{ext}", async context =>
            //    {
            //        if(context.Request.RouteValues["ext"] as string == "exe")
            //        {
            //            await context.Response.WriteAsync("It is an exe\n");
            //        }
            //        await context.Response.WriteAsync($"You can dounload the " +
            //            $"{context.Request.RouteValues["name"].ToString() + "." + context.Request.RouteValues["ext"].ToString()} now...");
            //    });
            //    //endpoint.MapGet("capital/{country}", CapitalStat.Endpoint);
            //    endpoint.MapGet("size/{city?}", PopulationStat.Endpoint)
            //    .WithMetadata(new RouteNameMetadata("population"));
            //});

            app.Run(async context =>
            {
                await context.Response.WriteAsync("\nTerminal end reached");
            });

            //app.UseEndpoints(endpoint => {
            //    endpoint.MapGet("endpoint", async context =>
            //    {
            //        await context.Response.WriteAsync("\nEndpoint\n");
            //    });
            //    endpoint.MapGet("{a}/{b}", async context =>
            //    {
            //        foreach (var item in context.Request.RouteValues)
            //        {
            //            await context.Response.WriteAsync(item.Key + " : " + item.Value.ToString());
            //        }
            //    });
            //    //endpoint.MapGet("{a}/{b}", new Capital().Invoke);
            //    //endpoint.MapGet("population/paris", new Population().Invoke);
            //});




            //app.MapWhen(context => context.Request.Query["custom"] == "true", app =>
            //    app.UseMiddleware<QueryStringMiddleWare>());

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



            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
