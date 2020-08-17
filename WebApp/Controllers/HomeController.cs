using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [HttpsOnly]
    public class HomeController : Controller
    {
        [ServiceFilter(typeof(GuidResponseAttribute))]
        [ServiceFilter(typeof(GuidResponseAttribute))]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on Home controller");
        }

        
        public IActionResult Secure()
        {
            return View("Message", "This is the Secure action on Home controller");
            
        }

        [ChangeArg]
        public IActionResult Message(string message1, string message2 = "None")
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("message1", message1);
            dictionary.Add("message2", message2);

            return View("Message", dictionary);
        }

        [RangeException]
        public ViewResult GenerateException(int? id)
        {
            if(id == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            else if(id > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            else
            {
                return View("Message", $"The vlaue is {id}");
            }
        }
    }
}
