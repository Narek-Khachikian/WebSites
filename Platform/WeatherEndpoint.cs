using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Services;

namespace Platform
{
    public class WeatherEndpoint
    {
        public static async Task Endpoint(HttpContext context, IResponseFormatter formater)
        {
            //IResponseFormatter formater = context.RequestServices.GetRequiredService<IResponseFormatter>();
            await formater.Format(context,"Endpoint, it is cloudy in London");
        }
    }
}
