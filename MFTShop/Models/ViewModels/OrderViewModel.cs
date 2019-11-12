﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public ICollection<OrderDetailViewModel> Details { get; set; }
    }
}
