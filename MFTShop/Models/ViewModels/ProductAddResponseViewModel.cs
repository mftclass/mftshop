﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.ViewModels
{
    public class ProductAddResponseViewModel
    {
        public string Message { get; set; }
        public bool Succeed { get; set; }
        public int orderId { get; set; }
    }
}
