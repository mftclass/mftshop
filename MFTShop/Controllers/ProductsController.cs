using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MFTShop.Models.ViewModels;
using MFTShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace MFTShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices productServices;
        private readonly IOrderServices orderServices;

        public ProductsController(IProductServices productServices, IOrderServices orderServices)
        {
            this.productServices = productServices;
            this.orderServices = orderServices;
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
        [Produces("application/json")]
        public IActionResult AddProductToOrder(int productId, int quantity = 1)
        {
            UserName = User.FindFirstValue(ClaimTypes.Name);
            var result = orderServices.saveOrder(UserName, productId, quantity);
            return Json(result);
        }

        [Produces("application/json")]
        public IActionResult GetProductOfOpenOrder()
        {
            UserName = User.FindFirstValue(ClaimTypes.Name);

            var order = orderServices.getOrder(UserName);
            var orderdetails = orderServices.getOrderDetails(order.Id, UserName);

            var result = orderdetails.Details;
            return Json(result);
        }

    }
}