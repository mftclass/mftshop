using MFTShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZarinpalSandbox;
using ZarinpalSandbox.Models;
namespace MFTShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderReportServices orderReportServices;
        private readonly IOrderServices orderServices;
        string Username
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.Name);
            }
        }

        public OrdersController(IOrderReportServices orderReportServices,IOrderServices orderServices)
        {
            this.orderReportServices = orderReportServices;
            this.orderServices = orderServices;
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
        public IActionResult ShowFactor()
        {
            var order = orderServices.getOrder(Username);
            var model = orderServices.getOrderDetails(order.Id,Username);
            return View(model);
        }
        public async Task<IActionResult> CheckOut()
        {
            var totalPrice = orderServices.getOrder(Username).AmountBuy;
            Payment payment = new Payment(totalPrice);
            var result = await payment.PaymentRequest("khkhkhk", "https://localhost:10000/Orders/CheckOutCallback");
            
            if (result.Status == 100)
                return Redirect(result.Link);
            else
                return Redirect("error");
        }
        public IActionResult CheckOutCallback(PaymentVerificationResponse verifyModel)
        {
            

        }

    }
}
