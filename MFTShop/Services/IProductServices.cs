using System.Collections.Generic;
using MFTShop.Models.ViewModels;

namespace MFTShop.Services
{
    public interface IProductServices
    {
        List<ProductViewModel> GetProducts(int categoryId);
    }
}
