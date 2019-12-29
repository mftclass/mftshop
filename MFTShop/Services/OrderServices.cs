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
    public partial class OrderServices : IOrderServices
    {
        private readonly IApplicationDbContext db;
        public OrderServices(IApplicationDbContext _db)
        {
            db = _db;
        }
        public ProductAddResponseViewModel saveOrder(string Username, int productId, int quantity = 1)
        {
            var responseModel = new ProductAddResponseViewModel();

            var customer = this.getCustomer(Username);
            var product = this.getProduct(productId);
            Order order;
            if (customer == null)
            {
                responseModel.Message = "Customer is wrong";
                responseModel.Succeed = false;

                return responseModel;
            }


            if (product == null)
            {
                responseModel.Message = "Product is wrong";
                responseModel.Succeed = false;

                return responseModel;
            }

            order = getOrder(Username);
            if (order == null)
            {
                order = new Order()
                {
                    OrderDate = DateTime.Now,
                    AmountBuy = product.Price,
                    Customer = customer,

                };
                db.Orders.Add(order);
            }

            var orderDetail = new OrderDetail()
            {
                Order = order,
                Product = product,
                UnitPriceBuy = quantity * product.Price,
                Tax = 0,
                Discount = 0,
                Quantity = quantity,
                CreationDate = DateTime.Now
            };
            db.OrderDetails.Add(orderDetail);
            db.SaveChanges();


            responseModel.Message = "Product added to order";
            responseModel.Succeed = true;
            responseModel.orderId = order.Id;

            return responseModel;


        }

        public OrderViewModel getOrderDetails(int OrderId, string Username)
        {
            OrderViewModel responseModel = new OrderViewModel();


            var order = getOrder(Username, OrderId, withIncludes: true);
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

        public int validateOrders()
        {
            var orders = db.Orders.Include(o => o.OrderDetails).ToList();
            //In OrderStatusType Enum, there is a 1000 step distance between temp values and
            //real values, so they can simply turn into each other
            //for more info,
            ///<seealso cref="OrderStatusTypes"/>
            const int Status_Temp_Distance = 1000;
            orders.AsParallel()
                .ForAll(o =>
                {
                    var detailsPriceSum = o.OrderDetails.Where(od => !od.DeleteDate.HasValue).Sum(odp => odp.UnitPriceBuy);
                    if (o.AmountBuy != detailsPriceSum)
                    {
                        o.AmountBuy = detailsPriceSum;
                        o.OrderStatus = o.OrderStatus + Status_Temp_Distance;

                    }
                });
            var totalChanged = 0;
            orders.ForEach(o =>
            {
                if ((int)o.OrderStatus >= Status_Temp_Distance)
                {
                    totalChanged++;
                    o.OrderStatus = o.OrderStatus - Status_Temp_Distance;
                }
            });

            db.SaveChanges();
            return totalChanged;

        }

        public bool addAuthorityToOrder(string username, string authority, int? orderId = null)
        {
            var order = getOrder(username, orderId);
            if (order == null)
            {
                return false;
            }
            order.Authority = authority;
            db.SaveChanges();
            return true;
        }

        public Order getOrder(string username, int? orderId = null, OrderStatusTypes? status = OrderStatusTypes.Open, bool withIncludes = false)
        {
            Order order = null;
            IQueryable<Order> orders = db.Orders.Where(x => x.Customer.UserName == username);
            if (orderId.HasValue)
            {
                orders = orders.Where(x => x.Id == orderId.Value);
            }
            if (status.HasValue)
            {
                orders = orders.Where(x => x.OrderStatus == status.Value);
            }
            if (withIncludes)
            {
                orders = orders.Include(x => x.OrderDetails)
                               .ThenInclude(x => x.Product);
            }
            var ordersList = orders.ToList();
            switch (ordersList.Count())
            {
                case 0:
                    return order;
                case 1:
                    return orders.SingleOrDefault();
                default:
                    foreach (var item in orders)
                    {
                        item.OrderStatus = OrderStatusTypes.NeedReview;
                    }
                    db.SaveChanges();
                    return order;
            }
        }

        public Customer getCustomer(string Username)
        {
            return db.Customers.SingleOrDefault(c => c.UserName == Username);

        }
        public Product getProduct(int productId)
        {
            return db.Products.SingleOrDefault(p => p.Id == productId && !p.DisableDate.HasValue && !p.RemoveDate.HasValue);
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
