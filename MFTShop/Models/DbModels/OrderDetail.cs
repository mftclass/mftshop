using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.DbModels
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        //Product Price When Product was Sold
        public decimal UnitPriceBuy { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        //the time that product added to order
        public DateTime CreationDate { get; set; }
    }
}
