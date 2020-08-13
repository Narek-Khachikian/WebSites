using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;


namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class FormController : Controller
    {
        private DataContext dbContext;
        public FormController(DataContext ctx)
        {
            dbContext = ctx;
        }

        public async Task<IActionResult> Index(long? id = 1)
        {
            return View("Form", await dbContext.Products.Include(p=>p.Category).Include(p=>p.Supplier).FirstOrDefaultAsync(p=>p.ProductId == id));
        }

        [HttpPost]
        public IActionResult SubmitForm(Product product)
        {
            TempData["ItemId"] = product.ProductId.ToString();
            TempData["name"] = product.Name;
            TempData["price"] = product.Price.ToString();
            TempData["categoryId"] = product.CategoryId.ToString();
            TempData["supplierId"] = product.SupplierId.ToString();
            
            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View(TempData);
        }

    }
}
