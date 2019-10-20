using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public OrderViewModel Order { get; set; }
        public ProductViewModel Product { get; set; }
        public decimal UnitPriceBuy { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
    }
}
