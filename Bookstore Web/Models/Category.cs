using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bookstore_Web.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "must be 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
