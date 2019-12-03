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
        string Username
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.Name);
            }
        }

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
            ProductAddResponseViewModel result = orderServices.saveOrder(Username, productId, quantity);
            return Json(result);
        }

        [Produces("application/json")]
        public IActionResult GetProductOfOpenOrder()
        {

            var order = orderServices.getOrder(Username);
            OrderViewModel result = orderServices.getOrderDetails(order.Id, Username);

            return Json(result);
        }

    }
}