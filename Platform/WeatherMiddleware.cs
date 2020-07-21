using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Platform.Services;

namespace Platform
{
    public class WeatherMiddleware
    {
        private RequestDelegate next;
        private IResponseFormatter formater;

        public WeatherMiddleware(RequestDelegate nextDelegate, IResponseFormatter formater)
        {
            next = nextDelegate;
            this.formater = formater;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path == "/middleware/class")
            {
                await formater.Format(context,"Middleware It is raining in London");
            }
            else
            {
                await next(context);
            }
        }
    }
}
