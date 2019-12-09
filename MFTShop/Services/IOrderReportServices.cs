using System.Collections.Generic;
using MFTShop.Models.DbModels;
using MFTShop.Models.ViewModels;

namespace MFTShop.Services
{
    public interface IOrderReportServices
    {
        OrderViewModel CustomerOrderDetails(int OrderId, string Username);
        List<Order> CustomerOrders(string username, int? orderId = null, OrderStatusTypes? status = OrderStatusTypes.Open, bool withIncludes = false);
    }
}