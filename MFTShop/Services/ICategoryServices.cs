using System.Collections.Generic;
using MFTShop.Models.ViewModels;

namespace MFTShop.Services
{
    public interface ICategoryServices
    {
        List<CategoryViewModel> GetCategories();
    }
}