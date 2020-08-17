using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Filters
{
    public class SimpleCacheAsyncAttribute : Attribute, IAsyncResourceFilter
    {
        private Dictionary<PathString, IActionResult> asyncCache = new Dictionary<PathString, IActionResult>();
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            PathString path = context.HttpContext.Request.Path;
            if (asyncCache.ContainsKey(path))
            {
                context.Result = asyncCache[path];
                asyncCache.Remove(path);
            }
            else
            {
                ResourceExecutedContext ctx = await next();

                asyncCache.Add(path, ctx.Result);
            }
        }
    }
}
