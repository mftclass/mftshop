using System.Collections.Generic;
using MFTShop.Models.ViewModels;

namespace MFTShop.Services
{
    public interface IProductServices
    {
        int GetCategoryId(string CategoryName);
        List<ProductViewModel> GetProducts(int categoryId, bool Deleted = false);
        List<ProductViewModel> GetProducts(string CategoryName, bool Deleted = false);
    }
}