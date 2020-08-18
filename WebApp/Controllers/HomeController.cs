using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApp.Filters;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

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
        private IEnumerable<Product> Products => dbContext.Products;

        public IActionResult Index()
        {
            return View(dbContext.Products.Include(p => p.Category).Include(p => p.Supplier));
        }
    }
}
