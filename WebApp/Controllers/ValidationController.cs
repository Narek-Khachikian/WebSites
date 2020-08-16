using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationController : ControllerBase
    {
        private DataContext dbContext;

        public ValidationController(DataContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet("CategoryKey")]
        public bool CategoryKey(string CategoryId)
        {
            long keyVal;
            return long.TryParse(CategoryId, out keyVal) 
                    && dbContext.Categories.Find(keyVal) != null;
        }

        [HttpGet("SupplierKey")]
        public bool SupplierKey(string SupplierId)
        {
            long keyVal;
            return long.TryParse(SupplierId, out keyVal) 
                    && dbContext.Suppliers.Find(keyVal) != null;
        }
    }
}
