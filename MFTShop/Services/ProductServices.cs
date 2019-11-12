using MFTShop.Data;
using MFTShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IApplicationDbContext db;
        public ProductServices(IApplicationDbContext _db)
        {
            db = _db;
        }
        public List<ProductViewModel> GetProducts(int categoryId, bool Deleted = false)
        {
            return db.Products
                .Where(p => !p.RemoveDate.HasValue && //not deleted
                            !p.DisableDate.HasValue && //not disabled
                             p.ProductCategories
                             .Any(pc =>
                                  pc.Cateory.Id == categoryId &&
                                 !pc.DisableDate.HasValue && //not disabled
                                 !pc.RemoveDate.HasValue && //not deleted
                                 !pc.Cateory.RemoveDate.HasValue && //not deleted
                                 !pc.Cateory.DisableDate.HasValue //not disabled
                                )
                       )
                .Select(x => new ProductViewModel()
                {
                    ImageAddress = x.PictureAddress,
                    Title = x.Title,
                    Id=x.Id,
                    price=x.Price

                }).ToList();
        }
        public List<ProductViewModel> GetProducts(string CategoryName, bool Deleted = false)
        {
            return GetProducts(GetCategoryId(CategoryName), Deleted);
        }
        public int GetCategoryId(string CategoryName)
        {
            return db.Categories.Single(c => c.Title == CategoryName).Id;
        }
    }
}
