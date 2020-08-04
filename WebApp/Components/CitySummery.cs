using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Components
{
    public class CitySummery : ViewComponent
    {
        private CityData cityData;

        public CitySummery(CityData ctx)
        {
            cityData = ctx;
        }

        public IViewComponentResult Invoke(string themName)
        {
            ViewBag.themName = themName;
            return View(new CityVewModel()
            {
                Cities = cityData.Cities.Count(),
                Population = cityData.Cities.Sum(c=>c.Population)
            });
            
        }
    }
}
