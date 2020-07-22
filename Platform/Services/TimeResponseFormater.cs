using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Services
{
    public class TimeResponseFormater : IResponseFormatter
    {
        private ITimeStamp timeStamp;

        public TimeResponseFormater(ITimeStamp timeStamp)
        {
            this.timeStamp = timeStamp;
        }

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"{timeStamp.TimeStamp} : {content}");
        }
    }
}
