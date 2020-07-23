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
using Platform.Services;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Platform
{
    public class Startup
    {

        private IConfiguration Config;

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MessageOptions>(Config.GetSection("Location"));
            
            
            //services.Configure<MessageOptions>(opt => opt.CityName = "Moscow");
            //services.Configure<RouteOptions>(opt =>
            //opt.ConstraintMap.Add("countryName",typeof(CountryRouteConstraint))
            //);
            //services.AddScoped<IResponseFormatter, TimeResponseFormater>();
            //services.AddScoped<ITimeStamp, DefaultTimeStamp>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions() { RequestPath = "/file" });
            app.UseRouting();

            //app.UseMiddleware<LocationMiddleware>();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapGet("/", async context =>
                {
                    logger.LogDebug("Response for / started");
                    await context.Response.WriteAsync("Hello World");
                    logger.LogDebug("Response for / finished");
                });
            });

            //app.Use(async (context, next) =>
            //{
            //    string str = Config["Logging:LogLevel:Default"];
            //    await context.Response.WriteAsync("microsoft log level is : " + str + "\n");
            //    string envMode = Config["ASPNETCORE_ENVIRONMENT"];
            //    await context.Response.WriteAsync($"The env setting is: {envMode}\n");
            //    string envMode1 = Config["WebService:Id"];
            //    await context.Response.WriteAsync($"The env setting is: {envMode1}\n");
            //    await next();
            //});

            //app.UseMiddleware<Population>();
            //app.UseMiddleware<Capital>();
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello\n");
            //    await next();
            //});


            //app.UseMiddleware<WeatherMiddleware>();

            //IResponseFormatter formater = new TextResponseFormater();
            //app.Use(async (context, next) =>
            //{
            //    if(context.Request.Path == "/middleware/function")
            //    {
            //        IResponseFormatter formater = context.RequestServices.GetService<IResponseFormatter>();
            //        await formater.Format(context,"Middleware Function: It is snowing in Chicago");
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});

            //app.UseEndpoints(endpoint =>
            //{
            //    //endpoint.MapGet("/endpoint/class", WeatherEndpoint.Endpoint);
            //    endpoint.MapEndpoint<WeatherEndpoint>("/endpoint/class","Endpoint2");
            //    endpoint.MapGet("/endpoint/function", async context =>
            //    {
            //        IResponseFormatter formater = context.RequestServices.GetService<IResponseFormatter>();
            //        await formater.Format(context,"Endpoint Function: it is sunny in LA");
            //    });
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("\nTerminal end reached");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Endpoint end = context.GetEndpoint();
            //    await context.Response.WriteAsync($"used {end.DisplayName}");
            //    await next();
            //});

            //app.UseEndpoints(endpoint =>
            //{
            //    endpoint.MapGet("{first:countryName}/{sec:bool?}/{third:length(10)?}/{*catchall}", async context =>
            //    {
            //        foreach(var item in context.Request.RouteValues)
            //        {
            //            if(item.Value != null)
            //            {
            //                await context.Response.WriteAsync($"Key:{item.Key} - Value:{item.Value}\n");
            //            }
            //        }
            //    }).WithDisplayName("main Endpoint\n")/*.Add(endpointBuilder => ((RouteEndpointBuilder)endpointBuilder).Order = 1)*/;

            //    endpoint.MapFallback(async context =>
            //    {
            //        await context.Response.WriteAsync("Error path");
            //    }).WithDisplayName("fallback endpoint\n");
            //});
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
