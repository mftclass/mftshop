using MFTShop.Models;
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
        private readonly IOrderServices orderReportServices;
        private readonly IOrderServices orderServices;
        string Username
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.Name);
            }
        }

        public OrdersController(IOrderServices orderReportServices,IOrderServices orderServices)
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

            var payment = await new Payment(totalPrice)
                .PaymentRequest("متن تست موقع خرید",
                    Url.Action(nameof(CheckOutCallback), "Orders", new { amount = totalPrice }, Request.Scheme));
            
            return payment.Status == 100 ? (orderServices.addAuthorityToOrder(Username, payment.Authority) ? (IActionResult)Redirect(payment.Link) : BadRequest("خطا در پرداخت")) : BadRequest($"خطا در پرداخت. کد خطا:{payment.Status}");
        }
        public async Task<IActionResult> CheckOutCallback(int amount, string Authority, string Status)
        {
            //توجه
            //بهتر است که به جای ارسال مبلغ به این متد، در این متد هم مبلغ را محاسبه کنید و سپس ادامه دهید.
            //****************
            if (Status == "NOK") return View("Error",new ErrorViewModel() {RequestId="khkhkhkhkh" });
            //گرفتن تاییدیه پرداخت
            var verification = await new Payment(amount)
                .Verification(Authority);
            //ارسال به صفحه خطا
            if (verification.Status != 100) return View("Error");
            //ارسال کد تراکنش به جهت نمایش به کاربر
            var refId = verification.RefId;
            return Ok("پرداخت شد");
        }

    }
}
