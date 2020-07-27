using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext dbContext;

        public ProductsController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return dbContext.Products;
        }

        [HttpGet("{Id:long}")]
        public Product GetProduct([FromServices] ILogger<ProductsController> logger, long Id)
        {
            logger.LogDebug("get product action invoked");
            //long Id = long.Parse(HttpContext.Request.RouteValues["Id"] as string);
            return dbContext.Products.Find(Id);
        }

        [HttpPost]
        public async Task PostProduct([FromBody] Product product)
        {
            dbContext.Add(product);
            await dbContext.SaveChangesAsync();
        }

        [HttpPut]
        public void PutProduct([FromBody] Product product)
        {
            dbContext.Products.Update(product);
            dbContext.SaveChanges();
        }

        [HttpDelete("{Id:long}")]
        public void DeleteProduct(long Id)
        {
            if(dbContext.Products.Find(Id) == null)
            {
                //RedirectToRoute("api/products");
                //RedirectToAction("GetProducts", "ProductsController");
            }
            else
            {
                dbContext.Products.Remove(dbContext.Products.Find(Id));
                dbContext.SaveChanges();
            }
        }
    }
}
