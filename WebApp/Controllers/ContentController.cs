using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private DataContext dbContext;

        public ContentController(DataContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet("string")]
        public string GetString() => "This is a string resource";

        [HttpGet("object/{format?}")]
        [FormatFilter]
        [Produces("application/json", "application/xml")]
        public object GetObject() => new { Id = 10, Name = "Narek" };


        [HttpPost]
        [Consumes("application/json")]
        public string SaveProductJson(ProductBindingTarget product)
        {
            return $"JSON: {product.Name}";
        }

        //[HttpPost]
        //[Consumes("application/xml")]
        //public string SaveProductXml(ProductBindingTarget product)
        //{
        //    return $"XML: {product.Name}";
        //}
    }
}
