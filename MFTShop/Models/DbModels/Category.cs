using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.DbModels
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [MinLength(5)]
        [Required]
        [Display(Name = "عنوان دسته")]
        public string Title { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreationDate { get; set; }
        public DateTime? DisableDate { get; set; }
        public DateTime? RemoveDate { get; set; }
    }
}
