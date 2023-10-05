using System.ComponentModel.DataAnnotations;

namespace testweb.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
