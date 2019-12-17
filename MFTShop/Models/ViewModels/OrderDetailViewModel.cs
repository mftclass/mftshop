﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public string ImageAddress { get; set; }
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        [Display(Name = "قیمت")]
        public int Price { get; set; }

    }
}
