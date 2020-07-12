using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;

        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }


        public IActionResult Index(string category, int productPage = 1) =>
            View(new ProductsListViewModel()
            {
                Products = !(category =="" || category==null) ? repository.Products.Where(p=> p.Category==null || p.Category == category).
                OrderBy(p => p.ProductId).Skip((productPage - 1) * PageSize).
                Take(PageSize): repository.Products.OrderBy(p=>p.ProductId).Skip((productPage-1)*PageSize).Take(PageSize) ,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null? repository.Products.Count() : repository.Products.Where(p=>p.Category == category).Count()
                }
            });
    }
}
