using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class FormController : Controller
    {
        private DataContext dbContext;
        public FormController(DataContext ctx)
        {
            dbContext = ctx;
        }

        public async Task<IActionResult> Index(long id)
        {
            return View("Form", await dbContext.Products.Include(p=>p.Category).Include(p=>p.Supplier).FirstAsync(p=>p.ProductId == id));
        }

        [HttpPost,AutoValidateAntiforgeryToken]
        public IActionResult SubmitForm()
        {
            foreach (string key in Request.Form.Keys
            )
            {
                TempData[key] = string.Join(", ", Request.Form[key]);
            }
            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View(TempData);
        }

    }
}
