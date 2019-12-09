using MFTShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MFTShop.Controllers
{
    public class OrdersReportController : Controller
    {
        private readonly IOrderReportServices orderReportServices;
        string Username
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.Name);
            }
        }

        public OrdersReportController(IOrderReportServices orderReportServices)
        {
            this.orderReportServices = orderReportServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUserOrdersReport()
        {
            var orders = orderReportServices.CustomerOrders(Username);
            return Json(orders);
        }
        public IActionResult GetUserDetailsReport(int OrderId)
        {
            var order = orderReportServices.CustomerOrderDetails(OrderId, Username);
            return Json(order);
        }
    }
}
