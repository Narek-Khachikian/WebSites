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
    public class SuppliersController : ControllerBase
    {
        private DataContext dbContext;

        public SuppliersController(DataContext context)
        {
            dbContext = context;
        }

        [HttpGet("{Id:long}")]
        public async Task<Supplier> GetSupplier(long Id)
        {
            return await dbContext.Suppliers.FindAsync(Id);
        }
    }
}
