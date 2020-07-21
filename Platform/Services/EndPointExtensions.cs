using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platform.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class EndPointExtensions
    {
        public static void MapWeather(this IEndpointRouteBuilder app, string path)
        {
            IResponseFormatter formater = app.ServiceProvider.GetService<IResponseFormatter>();
            app.MapGet(path, context => Platform.WeatherEndpoint.Endpoint(context, formater));
        }
    }
}
