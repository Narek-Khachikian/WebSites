using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Platform
{
    public class QueryStringMiddleWare
    {
        private RequestDelegate next;
        private MessageOptions options;
        
        
        public QueryStringMiddleWare(RequestDelegate nextDelegate, IOptions<MessageOptions> msgOption)
        {
            next = nextDelegate;
            options = msgOption.Value;
            
            
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Method == HttpMethods.Get
            && context.Request.Query["custom"] == "true")
            {
                await context.Response.WriteAsync("Class-based Middleware \n");
            }
            if (context.Request.Query.ContainsKey("location"))
            {
                await context.Response.WriteAsync($"City Name: {options.CityName}, Country:{options.CountryName}\n");
            }

            if (next != null)
            {
                await next(context);
            }
            await context.Response.WriteAsync("\nLalalala");
        }
    }

    public class LocationMiddleware
    {
        private RequestDelegate next;
        private MessageOptions options;
        public LocationMiddleware(RequestDelegate nextDelegate,
        IOptions<MessageOptions> opts)
        {
            next = nextDelegate;
            options = opts.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response
                .WriteAsync($"{options.CityName}, {options.CountryName}");
            }
            else
            {
                await next(context);
            }
        }
    }
}