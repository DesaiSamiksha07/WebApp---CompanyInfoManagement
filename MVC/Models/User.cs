
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
    }
}
