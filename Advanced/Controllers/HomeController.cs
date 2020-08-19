using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Advanced.Models;
using Advanced.Models.ViewModels;

namespace Advanced.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private DataContext dbContext { get; set; }

        public HomeController(DataContext ctx)
        {
            dbContext = ctx;
        }

        
        public IActionResult Index([FromQuery] string selectedCity)
        {
            return View(new PeopleListViewModel()
            {
                People = dbContext.People.Include(p => p.Department).Include(p => p.Location),
                Cities = dbContext.Locations.Select(l => l.City).Distinct(),
                SelectedCity = selectedCity
            });
        }
    }
}
