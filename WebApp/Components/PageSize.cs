using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Components
{
    public class PageSize:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("https://www.apress.com/gp");
            return View(message.Content.Headers.ContentLength);
        }
    }
}
