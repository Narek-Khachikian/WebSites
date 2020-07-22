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
        private IResponseFormatter formater;

        public WeatherEndpoint(IResponseFormatter formater)
        {
            this.formater = formater;
        }

        public async Task Endpoint(HttpContext context)
        {
            //IResponseFormatter formater = context.RequestServices.GetRequiredService<IResponseFormatter>();
            await formater.Format(context,"Endpoint, it is cloudy in London");
        }

        public async Task Endpoint2(HttpContext context)
        {
            await formater.Format(context, "Endpoint2, it is cloudy but hot in Yerevan");
        }
    }
}
