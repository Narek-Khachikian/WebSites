﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApp.Filters
{
    public class HttpsOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.IsHttps)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
