using MFTShop.Data;
using MFTShop.Models.DbModels;
using MFTShop.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Services
{
    public class OrderReportServices : IOrderReportServices
    {
        private readonly IApplicationDbContext db;
        public OrderReportServices(IApplicationDbContext _db)
        {
            db = _db;
        }

        public List<Order> CustomerOrders(string username, int? orderId = null, OrderStatusTypes? status = OrderStatusTypes.Open, bool withIncludes = false)
        {
            List<Order> order = null;
            IQueryable<Order> orders = db.Orders.Where(x => x.Customer.UserName == username);
            if (orderId.HasValue)
            {
                orders = orders.Where(x => x.Id == orderId.Value);
            }
            if (status.HasValue)
            {
                orders = orders.Where(x => x.OrderStatus != status.Value);
            }
            if (withIncludes)
            {
                orders = orders.Include(x => x.OrderDetails)
                               .ThenInclude(x => x.Product);
            }
            return order;
        }
        public OrderViewModel CustomerOrderDetails(int OrderId, string Username)
        {
            OrderViewModel responseModel = new OrderViewModel();

            //var order = CustomerOrders(Username, OrderId,0,true).Single<Order>();
            //if (order == null)
            //    return responseModel;
            //responseModel.TotalPrice = order.OrderDetails.Sum(x => x.UnitPriceBuy);
            //return responseModel;

            var order = CustomerOrders(Username).Where(x => x.Id == OrderId).Single<Order>();
            if (order == null)
                return responseModel;

            var details =
                order.OrderDetails
                     .Where(x => !x.DeleteDate.HasValue)
                     .Select(x => new OrderDetailViewModel()
                     {
                         Id = x.Id,
                         ImageAddress = x.Product.PictureAddress,
                         Price = x.UnitPriceBuy,
                         Title = x.Product.Title
                     });
            responseModel.Details = details.ToList();
            responseModel.TotalPrice = details.Sum(x => x.Price);
            responseModel.Id = OrderId;

            return responseModel;

        }
    }
}
