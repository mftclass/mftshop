using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public string ImageAddress { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }

    }
}
