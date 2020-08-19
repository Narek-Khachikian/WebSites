using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Pages
{
    public class EditorPageModel : PageModel
    {
        public DataContext dbContext { get; set; }
        public EditorPageModel(DataContext ctx)
        {
            dbContext = ctx;
        }

        public IEnumerable<Category> Categories => dbContext.Categories;
        public IEnumerable<Supplier> Suppliers => dbContext.Suppliers;

        public ProductViewModel ViewModel { get; set; }
    }
}
