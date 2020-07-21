using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform
{
    public class Population
    {
        private RequestDelegate next;
        public Population() { }
        public Population(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            string[] parts = context.Request.Path.ToString()
            .Split("/", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && parts[0] == "population")
            {
                string city = parts[1];
                int? pop = null;
                switch (city.ToLower())
                {
                    case "london":
                        pop = 8_136_000;
                        break;
                    case "paris":
                        pop = 2_141_000;
                        break;
                    case "monaco":
                        pop = 39_000;
                        break;
                }
                if (pop.HasValue)
                {
                    await context.Response
                    .WriteAsync($"City: {city}, Population: {pop}");
                    return;
                }
            }
            if (next != null)
            {
                await next(context);
            }
        }
    }

    public class PopulationStat
    {
        public static async Task Endpoint(HttpContext context)
        {
            string city = context.Request.RouteValues["city"] as string ?? "london";
            int? population = null;
            switch((city).ToLower())
            {
                case "london":
                    population = 8_100_000;
                    break;
                case "paris":
                    population = 3_200_000;
                    break;
                case "monaco":
                    population = 25_000;
                    break;
            }
            if(population != null)
            {
                await context.Response.WriteAsync($"population of {city} is {population:n0}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }

    public class CountryRouteConstraint : IRouteConstraint
    {
        private string[] countries = { "uk", "france", "monaco" };
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string val = values[routeKey] as string ?? "";
            return countries.Contains(val.ToLower());
        }
    }
}
