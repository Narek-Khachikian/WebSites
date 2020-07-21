using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Services
{
    public interface IResponseFormatter
    {
        Task Format(HttpContext context, string content);
    }
}
