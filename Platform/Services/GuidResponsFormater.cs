using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Services
{
    public class GuidResponsFormater : IResponseFormatter
    {
        private Guid id = Guid.NewGuid();
        
        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"{id}\n{content}");
        }
    }
}
