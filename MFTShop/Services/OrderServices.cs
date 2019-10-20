using MFTShop.Data;
using MFTShop.Models.DbModels;
using MFTShop.Models.ViewModels;
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
        public OrderViewModel saveOrder(int customerId, int productId)
        {
            var customer = this.getCustoer(customerId);
            if (customer != null)
            {
                var product = this.getProduct(productId);
                if (product != null)
                {
                    var order = new Order()
                    {
                        OrderDate = DateTime.Now,
                        AmountBuy = product.Price,
                        Customer = customer
                    };
                    db.SaveChanges();
                    var orederDetail = new OrderDetail()
                    {
                        Order = order,
                        Product = product,
                        UnitPriceBuy = product.Price,
                        Tax = 0,
                        Discount = 0,
                        Quantity = 1,
                        CreationDate = DateTime.Now
                    };
                    db.SaveChanges();
                    var detailsList = new List<OrderDetailViewModel>();
                    detailsList.Add(new OrderDetailViewModel()
                    {
                        Id = orederDetail.Id,
                        UnitPriceBuy = orederDetail.UnitPriceBuy,
                        Discount = orederDetail.Discount,
                        Tax = orederDetail.Tax,
                        Quantity = orederDetail.Quantity
                    });
                    return new OrderViewModel()
                    {
                        Id = order.Id,
                        Details = detailsList
                    };
                }
                throw new Exception("Product not found!");
            }
            throw new Exception("Custoer not found!");
        }
        private Customer getCustoer(int customerId)
        {
            return db.Customers.SingleOrDefault(c => c.Id == customerId.ToString());
        }
        private Product getProduct(int productId)
        {
            return db.Products.SingleOrDefault(p => p.Id == productId && !p.DisableDate.HasValue && !p.RemoveDate.HasValue);
        }
    }
}
