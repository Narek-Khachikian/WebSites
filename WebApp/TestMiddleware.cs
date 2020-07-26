﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp
{
    public class TestMiddleware
    {
        private RequestDelegate nextDelegate;

        public TestMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            if(context.Request.Path == "/test")
            {
                await context.Response.WriteAsync($"" +
                    $"There are {dataContext.Products.Count()} products in {dataContext.Categories.Count()} " +
                    $"categories from {dataContext.Suppliers.Count()} suppliers");
            }
            else
            {
                await nextDelegate(context);
            }
        } 
    }
}
