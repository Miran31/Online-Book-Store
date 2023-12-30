using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? ISBN { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        [Range(1,1000)]
        [Display(Name ="ListPrice")]
        public int ListPrice { get; set; }


        [Required]
        [Range(1, 1000)]
        [Display(Name = "Price for 1-50")]
        public int Price { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Price for 50+")]
        public int Price50 { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Price for 100+")]
        public int Price100 { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string? imageUrl { get; set; }
    }
}
