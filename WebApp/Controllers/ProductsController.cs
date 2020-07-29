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
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext dbContext;

        public ProductsController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return dbContext.Products;
        }

        [HttpGet("{Id:long}")]
        public async Task<IActionResult> GetProduct(long Id)
        {
            Product p = await dbContext.Products.FindAsync(Id);
            if(p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            return RedirectToAction(nameof(GetProduct),new { Id = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductBindingTarget target)
        {
            if (ModelState.IsValid)
            {
                Product p = target.ToProduct();
                await dbContext.Products.AddAsync(p);
                await dbContext.SaveChangesAsync();
                return Ok(p);
            }
            return BadRequest(ModelState);
            
        }

        [HttpPut]
        public async Task PutProduct(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }

        [HttpDelete("{Id:long}")]
        public async Task DeleteProduct(long Id)
        {
            dbContext.Products.Remove(dbContext.Products.Find(Id));
            await dbContext.SaveChangesAsync();
        }
    }
}
