using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CubeController : Controller
    {
        public IActionResult Index()
        {
            return View("Cube");
        }

        public IActionResult Cube(double num)
        {
            TempData["Num"] = num.ToString();
            TempData["Value"] = Math.Pow(num, 3).ToString();

            return RedirectToAction(nameof(Index));
        }
    }
}
