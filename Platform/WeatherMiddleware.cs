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
        //private IResponseFormatter formater;

        public WeatherMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
            //this.formater = formater;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formater1,
            IResponseFormatter formater2, IResponseFormatter formater3)
        {
            if(context.Request.Path == "/middleware/class")
            {
                await formater1.Format(context,"Middleware It is raining in London\n");
                await formater2.Format(context,"Middleware It is raining in London\n");
                await formater3.Format(context,"Middleware It is raining in London\n");
            }
            else
            {
                await next(context);
            }
        }
    }
}
