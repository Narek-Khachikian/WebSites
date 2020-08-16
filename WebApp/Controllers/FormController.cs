using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError(nameof(product.Name), "Product must have a name");
            }
            if(ModelState.GetValidationState(nameof(product.Price))==ModelValidationState.Valid && product.Price < 0.01m)
            {
                ModelState.AddModelError(nameof(product.Price), "Price should be a positive number");
            }
            if (ModelState.GetValidationState(nameof(Product.Name))
                == ModelValidationState.Valid
                && ModelState.GetValidationState(nameof(Product.Price))
                == ModelValidationState.Valid
                && product.Name.ToLower().StartsWith("small") && product.Price > 100)
            {
                ModelState.AddModelError("", "Small products cannot cost more than $100");
            }
            if (!dbContext.Categories.Any(c=>c.CategoryId == product.CategoryId))
            {
                ModelState.AddModelError(nameof(product.CategoryId), "Category Id is not valid");
            }
            if(!dbContext.Suppliers.Any(s=>s.SupplierId == product.SupplierId))
            {
                ModelState.AddModelError(nameof(product.SupplierId), "Supplier Id is not valid");
            }

            if (ModelState.IsValid)
            {
                TempData["ItemId"] = product.ProductId.ToString();
                TempData["name"] = product.Name;
                TempData["price"] = product.Price.ToString();
                TempData["categoryId"] = product.CategoryId.ToString();
                TempData["supplierId"] = product.SupplierId.ToString();
                return RedirectToAction(nameof(Results));
            }
            else
            {
                
                return View("Form");
            }
        }

        public IActionResult Results()
        {
            return View(TempData);
        }

    }
}
