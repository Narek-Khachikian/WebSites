using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApp.Filters;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private DataContext dbContext;
        public HomeController(DataContext ctx)
        {
            dbContext = ctx;
        }

        private IEnumerable<Category> Categories => dbContext.Categories;
        private IEnumerable<Supplier> Suppliers => dbContext.Suppliers;

        public IActionResult Index()
        {
            return View(dbContext.Products.Include(p => p.Category).Include(p => p.Supplier));
        }

        public async Task<IActionResult> Details(long id)
        {
            Product p = await dbContext.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => p.ProductId == id);
            ProductViewModel model = ViewModelFactory.Details(p);
            return View("ProductEditor", model);
        }

        public IActionResult Create()
        {
            return View("ProductEditor", ViewModelFactory.Create(new Product(), Categories, Suppliers));
        }

        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductEditor", ViewModelFactory.Create(product,Categories,Suppliers));
            }

            product.ProductId = default;
            product.Supplier = default;
            product.Category = default;

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(long id)
        {
            Product p = await dbContext.Products.FindAsync(id);
            if (p == null)
            {
                RedirectToAction(nameof(Index));
            }
            ProductViewModel model = ViewModelFactory.Edit(p, Categories, Suppliers);
            return View("ProductEditor", model);
        }


        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductEditor", ViewModelFactory.Edit(product, Categories, Suppliers));
            }

            product.Category = default;
            product.Supplier = default;
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            Product p = await dbContext.Products.FindAsync(id);
            ProductViewModel model = ViewModelFactory.Delete(p, Categories, Suppliers);
            return View("ProductEditor", model);
        }

        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete([FromForm] Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Delete), ViewModelFactory.Delete(product, Categories, Suppliers));
            }
            dbContext.Products.Remove(dbContext.Products.Find(product.ProductId));
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
