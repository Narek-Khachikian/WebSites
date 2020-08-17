using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WebApp.Filters
{
    public class ChangePageArgAttribute : Attribute, IAsyncPageFilter
    {
        public string Mmessage { get; set; }
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerArguments.ContainsKey("message1") && Mmessage != null)
            {
                context.HandlerArguments["message1"] = Mmessage;
            }
            await next();
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            
        }
    }
}
