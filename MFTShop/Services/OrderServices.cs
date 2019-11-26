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
    public class OrderServices : IOrderServices
    {
        private readonly IApplicationDbContext db;
        public OrderServices(IApplicationDbContext _db)
        {
            db = _db;
        }
        public ProductAddResponseViewModel AddProductToOrder(string Username, int productId, int quantity = 1)
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
                order
                if (order == null)
                {
                    responseModel.Message = "Order is wrong";
                    responseModel.Succeed = false;

                    return responseModel;
                }
            //}



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


            var order = getOrder(OrderId, Username, true);
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

        public Order getOrder(int? orderId, string username, bool withIncludes = false, OrderStatusTypes status = OrderStatusTypes.Open)
        {
            if (!orderId.HasValue)
            {
                Customer customer = getCustomer(username);
                var orders = db.Orders.Where(x => x.Customer.Id == customer.Id &&
                                    x.OrderStatus == OrderStatusTypes.Open);
                if(orders.Count()!=1)
                {
                    foreach(Order order in orders)
                    {
                        order.OrderStatus = OrderStatusTypes.NeedReview;
                    }
                    Order newOrder = new Order()
                    {
                        OrderDate = DateTime.Now,
                        Customer = customer
                    };
                }
               
            }
            else
            {
                IQueryable<Order> orders = db.Orders.Where(x => x.Id == orderId &&
                                                          x.Customer.UserName == username &&
                                                          x.OrderStatus == status
                                                         );
                if (!withIncludes)
                    return orders.SingleOrDefault();
                else
                    return orders.Include(x => x.OrderDetails)
                                .ThenInclude(x => x.Product)
                                .SingleOrDefault();
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
    }
}
