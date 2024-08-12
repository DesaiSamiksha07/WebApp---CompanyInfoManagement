
using System.ComponentModel.DataAnnotations;


namespace MVC.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
