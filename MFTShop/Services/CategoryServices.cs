using MFTShop.Data;
using MFTShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IApplicationDbContext db;
        public CategoryServices(IApplicationDbContext _db)
        {
            db = _db;
        }
        public List<CategoryViewModel> GetCategories()
        {
            return db.Categories
                .Where(x=>!x.RemoveDate.HasValue && //not deleted
                          !x.DisableDate.HasValue)  //and not disabled
                .Select(x => new CategoryViewModel()
            {
                ImageAddress = x.PictureAddress,
                ProductCount = x.ProductCategories.Count(c=>!c.DisableDate.HasValue &&
                                                            !c.RemoveDate.HasValue &&
                                                            !c.Prodct.DisableDate.HasValue &&
                                                            !c.Prodct.RemoveDate.HasValue),
                Title = x.Title
            }).ToList();
        }
    }
}
