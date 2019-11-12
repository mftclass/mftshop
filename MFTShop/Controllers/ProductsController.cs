using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFTShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace MFTShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices productServices;
        public ProductsController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryFilter(int CategoryId)
        {
            var products = productServices.GetProducts(CategoryId);
            return View(products);

        }
         

    }
}