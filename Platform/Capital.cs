﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using System.Runtime.InteropServices;

namespace Platform
{
    public class Capital
    {
        private RequestDelegate next;

        public Capital() { }
        public Capital(RequestDelegate nextDel)
        {
            next = nextDel;
        }

        public async Task Invoke(HttpContext context)
        {
            string[] parts = context.Request.Path.ToString()
                .Split("/", StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length == 2 && parts[0] == "capital")
            {
                string capital = null;
                string country = parts[1];
                switch (country.ToLower())
                {
                    case "uk":
                        capital = "London";
                        break;
                    case "france":
                        capital = "Paris";
                        break;
                    case "monaco":
                        context.Response.Redirect($"/population/{country}");
                        return;
                }
                if (capital != null)
                {
                    await context.Response
                    .WriteAsync($"{capital} is the capital of {country}");
                    return;
                }
            }
            if (next != null)
            {
                await next(context);
            }
        }
    }

    public static class CapitalStat
    {
        public static async Task Endpoint(HttpContext context)
        {
            string capital = null;
            string country = context.Request.RouteValues["country"] as string;
            switch ((country ?? "").ToLower())
            {
                case "uk":
                    capital = "London";
                    break;
                case "france":
                    capital = "Paris";
                    break;
                case "monaco":
                    //context.Response.Redirect($"/population/{country}");
                    LinkGenerator generator = context.RequestServices.GetService<LinkGenerator>();
                    string url = generator.GetPathByRouteValues(context, "population", new { city = country });
                    context.Response.Redirect(url);
                    return;
            }
            if(capital != null)
            {
                await context.Response.WriteAsync($"{capital} is the capital of {country}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
