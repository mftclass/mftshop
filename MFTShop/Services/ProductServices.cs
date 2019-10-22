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
        public List<ProductViewModel> GetProducts(int categoryId)
        {
            return db.Products
                .Where(x => !x.RemoveDate.HasValue && //not deleted
                          !x.DisableDate.HasValue && //not disabled
                          x.ProductCategories.Any(c => c.Cateory.Id == categoryId &&
                            !c.DisableDate.HasValue && //not disabled
                            !c.RemoveDate.HasValue && //not deleted
                            !c.Cateory.RemoveDate.HasValue && //not deleted
                            !c.Cateory.DisableDate.HasValue //not disabled                     
                            )&&
                            !db.OrderDetails.Any(od => od.Product.Id == x.Id)
                          
                       )  
                .Select(x => new ProductViewModel()
                {
                    ImageAddress = x.PictureAddress,
                    Title = x.Title
                }).ToList();
        }
    }
}
