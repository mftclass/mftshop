using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.ViewModels
{
    public class IndexViewModel
    {
        public string PageTitle { get; set; }
        public List<CategoryViewModel> categoryViewModels { get; set; }
    }
}
