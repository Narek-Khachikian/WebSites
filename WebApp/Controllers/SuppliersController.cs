using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

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
            Supplier supplier = await dbContext.Suppliers.Include(s => s.Products).FirstAsync(s => s.SupplierId == Id);
            foreach (var item in supplier.Products)
            {
                item.Supplier = null;
            }
            return supplier;
        }

        [HttpPatch("{Id:long}")]
        public async Task<Supplier> PatchSupplier(long Id, JsonPatchDocument<Supplier> patchDoc)
        {
            Supplier s = await dbContext.Suppliers.FindAsync(Id);
            if(s != null)
            {
                patchDoc.ApplyTo(s);
                await dbContext.SaveChangesAsync();
            }
            return s;
        }
    }
}
