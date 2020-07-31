using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController:Controller
    {
        private DataContext dbContext;

        public HomeController(DataContext ctx)
        {
            dbContext = ctx;
        }

        public async Task<IActionResult> Index(long id=1)
        {
            return View(await dbContext.Products.FindAsync(id));
        }

        public IActionResult Common()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            return View(await dbContext.Products.ToArrayAsync());
        }

    }
}
