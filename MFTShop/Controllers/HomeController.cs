﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MFTShop.Models;
using MFTShop.Data;
using MFTShop.Models.DbModels;
using MFTShop.Services;
using MFTShop.Models.ViewModels;

namespace MFTShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationDbContext db;
        private readonly ICategoryServices categoryServices;
        public HomeController(ILogger<HomeController> logger, IApplicationDbContext _db, ICategoryServices categoryServices)
        {
            db = _db;
            this.categoryServices = categoryServices;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                categoryViewModels = categoryServices.GetCategories()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult seeddb(string secret)
        {
            if (secret == "farhad")
            {
                List<Category> categories = new List<Category>()
                {
                    new Category()
                    {
                        Title = "خانگی",
                        CreationDate = DateTime.Now,
                    },
                    new Category()
                    {
                        Title = "ورزشی",
                        CreationDate = DateTime.Now,
                    },
                    new Category()
                    {
                        Title = "اداری",
                        CreationDate = DateTime.Now,
                    },
                     new Category()
                    {
                        Title = "اسباب‌بازی",
                        CreationDate = DateTime.Now,
                    }
                };

                if (db.Categories.Count() < 4)
                {
                    db.Categories.AddRange(categories);
                }
                db.SaveChanges();

                for (int i = 0; i < 50; i++)
                {
                    Product prod = new Product()
                    {
                        CreationDate = DateTime.Now,
                        Title = $"Product{i}"
                    };
                    db.Products.Add(prod);
                    db.SaveChanges();

                    ProductCategory productCategory = new ProductCategory()
                    {
                        Prodct = prod,
                        CreationDate = DateTime.Now,
                        Cateory = categories[new Random().Next(0, 3)],

                    };
                    db.ProductCategories.Add(productCategory);
                    db.SaveChanges();
                }



            }
            return Ok();
        }
    }
}
