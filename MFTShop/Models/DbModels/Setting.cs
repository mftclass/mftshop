using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.DbModels
{
    public class Setting
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [MinLength(3)]
        [Required]
        public string Key { get; set; }
        [MaxLength(50)]
        [MinLength(1)]
        [Required]
        public string Value { get; set; }
    }
}
